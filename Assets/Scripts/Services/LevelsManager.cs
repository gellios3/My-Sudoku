using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

namespace Services
{
    public class LevelsManager
    {
        private string _path;

        public Level SelectedLevel { get; set; }

        public List<Level> levels { get; } = new List<Level>();

        /// <summary>
        /// Level manager
        /// </summary>
        [Inject]
        public SudokuSolverService SudokuSolverService { get; set; }

        /// <summary>
        /// Init Levels
        /// </summary>
        public void InitLevels()
        {
            _path = Path.Combine(Application.persistentDataPath, "Levels");
            DeserializeLevels();
        }

        /// <summary>
        /// Deserialize all levels
        /// </summary>
        private void DeserializeLevels()
        {
            while (true)
            {
                if (Directory.Exists(_path))
                {
                    DeserializeLevel();
                }
                else
                {
                    CopyLevelsFromResources();
                    continue;
                }

                break;
            }
        }

        /// <summary>
        /// Deserialize level from json
        /// </summary>
        /// <returns></returns>
        private void DeserializeLevel()
        {
            var files = Directory.EnumerateFiles(_path, "*.json");
            Debug.LogWarning($"Deserialize levels from {_path}");

            foreach (var filepath in files)
            {
                Debug.LogWarning($"Load level from: {filepath}");

                var rawJson = File.ReadAllText(filepath);
                var node = JSON.Parse(rawJson);

                foreach (var nodeKey in node.Keys)
                {
                    var grid = new Level(node[nodeKey.Value]);
                    if (!SudokuSolverService.Solve((int[,]) grid.Board.Clone()))
                        continue;
                    levels.Add(grid);
                }
            }
        }

        /// <summary>
        /// Copy levels from resources
        /// </summary>
        private void CopyLevelsFromResources()
        {
            Debug.Log("Copy levels from resources");

            Directory.CreateDirectory(_path);
            CopyDifficultFromResources("Sudoku");
        }

        /// <summary>
        /// Copy difficult from resources
        /// </summary>
        /// <param name="name"></param>
        private void CopyDifficultFromResources(string name)
        {
            var file = Resources.Load<TextAsset>($"Levels/{name}");
            var filepath = Path.Combine(_path, file.name + ".json");

            File.WriteAllText(filepath, file.text);
        }
    }
}