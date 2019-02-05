using Services;
using Signals.MainGame;
using UnityEngine;
using Views.MainGame;

namespace Mediators.MainGame
{
    public class BoardMediator : TargetMediator<BoardView>
    {
        /// <summary>
        /// Show keyboard signal
        /// </summary>
        [Inject]
        public ShowKeyboardSignal ShowKeyboardSignal { get; set; }

        /// <summary>
        /// Board service
        /// </summary>
        [Inject]
        public BoardService BoardService { get; set; }

        /// <summary>
        /// Board finished loading signal
        /// </summary>
        [Inject]
        public BoardFinishedLoadingSignal BoardFinishedLoadingSignal { get; set; }

        /// <summary>
        /// Player starts service
        /// </summary>
        [Inject]
        public PlayerStartsService PlayerStartsService { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.OnBoardReadyToPlay += () =>
            {
                PlayerStartsService.BoardReadyToPlay();
                LoadTilesToArray();
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BoardService.CheckBoard();
            }
        }

        /// <summary>
        /// Load tiles to array
        /// </summary>
        private void LoadTilesToArray()
        {
            foreach (Transform group in transform)
            {
                var groupX = (int) char.GetNumericValue(group.name[0]);
                var groupY = (int) char.GetNumericValue(group.name[2]);

                foreach (Transform tile in group)
                {
                    var tileX = (int) char.GetNumericValue(tile.name[0]);
                    var tileY = (int) char.GetNumericValue(tile.name[2]);

                    var x = groupX * 3 + tileX;
                    var y = groupY * 3 + tileY;

                    BoardService.TilesView[x, y] = tile.GetComponent<BoardTileView>();
                    if (!View.constantBoard)
                    {
                        BoardService.TilesView[x, y].OnTilePressed += pressedTile =>
                        {
                            ShowKeyboardSignal.Dispatch(pressedTile);
                        };
                        BoardService.TilesView[x, y].ValueChanged += () => { BoardService.CheckBoard(); };
                    }
                    else
                    {
                        tile.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }

            BoardFinishedLoadingSignal.Dispatch();
        }
    }
}