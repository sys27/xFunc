// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace xFunc.Maths;

/// <summary>
/// The parser for mathematical expressions.
/// </summary>
public partial class Parser
{
    private delegate IExpression? ScopedCallback(Parser parser, ref TokenReader tokenReader);

    private ref struct TokenReader
    {
        private const int BufferSize = 64;

        private Lexer lexer;

        // points to read position (item is not null)
        private int readIndex;

        // points to last write position (item is not null)
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
                var hasNextItem = lexer.MoveNext();
                if (!hasNextItem)
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

        public bool Check(TokenKind kind)
        {
            var token = Read();
            var result = token.Is(kind);
            if (result)
                AdvanceToNextPosition();

            return result;
        }

        public IExpression? Scoped(Parser parser, ScopedCallback scopedCallback)
        {
            var savedReadIndex = readIndex;
            scopeCount++;

            var result = scopedCallback(parser, ref this);
            if (result is null)
                readIndex = savedReadIndex;

            scopeCount--;
            Debug.Assert(scopeCount >= 0, "The scope count should be greater than or equal to 0.");

            return result;
        }

        public bool IsEnd { get; private set; }
    }
}