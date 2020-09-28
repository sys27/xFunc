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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using xFunc.Maths.Tokenization;

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

            private Lexer lexer;

            // points to read position (item != null)
            private int readIndex;

            // points to last write position (item != null)
            private int writeIndex;

            private Token[] buffer;

            private int scopeCount;

            public TokenReader(ref Lexer lexer)
            {
                this.lexer = lexer;

                IsEnd = false;
                readIndex = -1;
                writeIndex = -1;
                buffer = ArrayPool<Token>.Shared.Rent(BufferSize);
                scopeCount = 0;
            }

            public void Dispose() => ArrayPool<Token>.Shared.Return(buffer);

            private void EnsureEnoughSpace()
            {
                if (writeIndex < buffer.Length)
                    return;

                var newBuffer = ArrayPool<Token>.Shared.Rent(buffer.Length * 2);
                Array.Copy(buffer, newBuffer, buffer.Length);
                ArrayPool<Token>.Shared.Return(buffer);
                buffer = newBuffer;
            }

            private ref readonly Token Read()
            {
                Debug.Assert(readIndex <= writeIndex, "The read index should be less than or equal to write index.");

                // read from enumerator and write to buffer
                if (readIndex == writeIndex)
                {
                    var result = lexer.MoveNext();
                    if (!result)
                        IsEnd = true;

                    if (scopeCount == 0)
                        Flush();

                    writeIndex++;

                    EnsureEnoughSpace();

                    buffer[writeIndex] = lexer.Current;
                }

                Debug.Assert(writeIndex >= 0, "The write index should be greater than or equal to 0.");

                // read from buffer
                return ref buffer[readIndex + 1];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void Flush() => readIndex = writeIndex = -1;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void AdvanceToNextPosition() => readIndex++;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void IncreaseScope() => scopeCount++;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void DecreaseScope()
            {
                scopeCount--;

                Debug.Assert(scopeCount >= 0, "The scope count should be greater than or equal to 0.");
            }

            private void Rollback(int index)
            {
                Debug.Assert(index >= -1 && index <= writeIndex, "The index should be between [-1, writeIndex].");

                DecreaseScope();

                readIndex = index;
            }

            public Scope CreateScope()
            {
                IncreaseScope();

                return new Scope(ref this);
            }

            public void Rollback(Scope scope) => scope.Rollback(ref this);

            public void Commit() => DecreaseScope();

            public Token GetCurrent() => Read();

            public Token GetCurrent(TokenKind kind)
            {
                var token = Read();
                if (token.Is(kind))
                {
                    AdvanceToNextPosition();

                    return token;
                }

                return Token.Empty;
            }

            public bool Check(TokenKind kind) => GetCurrent(kind).IsNotEmpty();

            public bool IsEnd { get; private set; }

            public readonly ref struct Scope
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