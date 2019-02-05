using System.Collections.Generic;
using Signals.MainGame;
using Views.MainGame;

namespace Services
{
    public class BoardService
    {
        public readonly BoardTileView[,] TilesView = new BoardTileView[9, 9];

        [Inject] public OnSudokuCorrectSignal OnSudokuCorrectSignal { get; set; }

        /// <summary>
        /// Player starts service
        /// </summary>
        [Inject]
        public PlayerStartsService PlayerStartsService { get; set; }

        public void Clear()
        {
            foreach (var ti in TilesView) ti.Clear();
        }

        /// <summary>
        /// Set level solve
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(int[,] level)
        {
            for (var y = 0; y < 9; ++y)
            {
                for (var x = 0; x < 9; ++x)
                {
                    if (level[x, y] > 0)
                    {
                        TilesView[x, y].SetConstantValue(level[x, y]);
                    }
                }
            }

            PlayerStartsService.BoardReadyToPlay();
        }

        /// <summary>
        /// Check board is solve
        /// </summary>
        public void CheckBoard()
        {
            if (!CheckRows())
                return;
            if (!CheckColumns())
                return;
            if (!CheckBoxes())
                return;

            OnSudokuCorrectSignal.Dispatch();
        }

        /// <summary>
        /// Check solve rows
        /// </summary>
        /// <returns></returns>
        private bool CheckRows()
        {
            var hash = new HashSet<int>();

            for (var y = 0; y < 9; ++y)
            {
                for (var x = 0; x < 9; ++x)
                {
                    var value = TilesView[x, y].Value;

                    if (value == 0 || hash.Contains(value))
                    {
                        return false;
                    }

                    hash.Add(value);
                }

                hash.Clear();
            }

            return true;
        }

        /// <summary>
        /// Check solve columns
        /// </summary>
        /// <returns></returns>
        private bool CheckColumns()
        {
            var hash = new HashSet<int>();

            for (var x = 0; x < 9; ++x)
            {
                for (var y = 0; y < 9; ++y)
                {
                    var value = TilesView[x, y].Value;

                    if (value == 0 || hash.Contains(value))
                    {
                        return false;
                    }

                    hash.Add(value);
                }

                hash.Clear();
            }

            return true;
        }

        /// <summary>
        /// Check solve boxes
        /// </summary>
        /// <returns></returns>
        private bool CheckBoxes()
        {
            for (var y = 0; y < 3; ++y)
            {
                for (var x = 0; x < 3; ++x)
                {
                    if (CheckSingleBox(x, y))
                        continue;
                    return
                        false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check single box
        /// </summary>
        /// <param name="boxX"></param>
        /// <param name="boxY"></param>
        /// <returns></returns>
        private bool CheckSingleBox(int boxX, int boxY)
        {
            var hash = new HashSet<int>();
            var startX = boxX * 3;
            var startY = boxY * 3;

            for (var y = startY; y < startY + 3; ++y)
            {
                for (var x = startX; x < startX + 3; ++x)
                {
                    var value = TilesView[x, y].Value;

                    if (value == 0 || hash.Contains(value)) return false;
                    hash.Add(value);
                }
            }

            return true;
        }
    }
}