// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths
{
    /// <summary>
    /// The parser for mathematical expressions.
    /// </summary>
    public partial class Parser
    {
        private ref struct TokenReader
        {
            private const int BufferSize = 64;

            private readonly IEnumerator<IToken> enumerator;
            private bool enumerableEnded;

            // points to read position (item != null)
            private int readIndex;

            // points to last write position (item != null)
            private int writeIndex;

            private IToken[] buffer;

            private int scopeCount;

            public TokenReader(IEnumerable<IToken> tokens)
            {
                enumerator = tokens.GetEnumerator();
                enumerableEnded = false;
                readIndex = -1;
                writeIndex = -1;
                buffer = ArrayPool<IToken>.Shared.Rent(BufferSize);
                scopeCount = 0;
            }

            public void Dispose()
            {
                enumerator.Dispose();
                ArrayPool<IToken>.Shared.Return(buffer);
            }

            private void EnsureEnoughSpace()
            {
                if (writeIndex < buffer.Length)
                    return;

                var newBuffer = ArrayPool<IToken>.Shared.Rent(buffer.Length * 2);
                Array.Copy(buffer, newBuffer, buffer.Length);
                ArrayPool<IToken>.Shared.Return(buffer);
                buffer = newBuffer;
            }

            private TToken Read<TToken>() where TToken : class, IToken
            {
                // readIndex > writeIndex
                Debug.Assert(readIndex <= writeIndex, "The read index should be less than or equal to write index.");

                // read from enumerator and write to buffer
                if (readIndex == writeIndex)
                {
                    var result = enumerator.MoveNext();
                    if (!result)
                    {
                        enumerableEnded = true;

                        return null;
                    }

                    if (scopeCount == 0)
                        Flush();

                    writeIndex++;

                    EnsureEnoughSpace();

                    return (buffer[writeIndex] = enumerator.Current) as TToken;
                }

                // readIndex < writeIndex
                Debug.Assert(writeIndex >= 0, "The write index should be greater than or equal to 0.");

                // read from buffer
                return buffer[readIndex + 1] as TToken;
            }

            private void Rollback(int index)
            {
                Debug.Assert(index >= -1 && index <= writeIndex, "The index should be between [-1, writeIndex].");

                scopeCount--;

                Debug.Assert(scopeCount >= 0, "The scope count should be greater than or equal to 0.");

                readIndex = index;
            }

            private void Flush()
            {
                readIndex = writeIndex = -1;
            }

            private void AdvanceToNextPosition()
            {
                readIndex++;
            }

            public Scope CreateScope()
            {
                scopeCount++;

                return new Scope(ref this);
            }

            public void Rollback(Scope scope)
            {
                scope.Rollback(ref this);
            }

            public void Commit()
            {
                scopeCount--;

                Debug.Assert(scopeCount >= 0, "The scope count should be greater than or equal to 0.");

                if (scopeCount == 0 && readIndex == writeIndex)
                    Flush();
            }

            public TToken GetCurrent<TToken>() where TToken : class, IToken
            {
                var token = Read<TToken>();
                if (token != null)
                    AdvanceToNextPosition();

                return token;
            }

            public bool Symbol(SymbolToken symbolToken)
            {
                var token = Read<SymbolToken>();
                var result = token == symbolToken;
                if (result)
                    AdvanceToNextPosition();

                return result;
            }

            public OperatorToken Operator(OperatorToken operatorToken)
            {
                var token = Read<OperatorToken>();
                if (token == operatorToken)
                {
                    AdvanceToNextPosition();

                    return token;
                }

                return null;
            }

            public KeywordToken Keyword(KeywordToken keywordToken)
            {
                var token = Read<KeywordToken>();
                if (token == keywordToken)
                {
                    AdvanceToNextPosition();

                    return token;
                }

                return null;
            }

            public bool IsEnd
                => enumerableEnded && readIndex == writeIndex;

            public readonly struct Scope
            {
                private readonly int position;

                public Scope(ref TokenReader tokenReader)
                    => position = tokenReader.readIndex;

                public void Rollback(ref TokenReader tokenReader)
                    => tokenReader.Rollback(position);
            }
        }
    }
}