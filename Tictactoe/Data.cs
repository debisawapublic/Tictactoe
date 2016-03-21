using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe {
    class Data {
        public const string BUTTON_NAME = "button"; //!< ボタンのプロパティ名
        public const int BINGO_NUMBER = 3;

        public static readonly Dictionary<BUTTON_STATE, string> buttonStateSign = new Dictionary<BUTTON_STATE, string>() {  //!< ボタンの状態記号テーブル
            {BUTTON_STATE.CIRCLE, "○" },
            {BUTTON_STATE.CROSS, "×" },
        };

        public static Dictionary<int, List<BODER_STATUS>> _boderStatusList = new Dictionary<int, List<BODER_STATUS>>() {
            // 一行目
            { 0, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_FRONT, BODER_STATUS.SLASH_FRONT, BODER_STATUS.WIDTH_FRONT} },
            { 1, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_MIDDLE, BODER_STATUS.WIDTH_FRONT} },
            { 2, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_BACK, BODER_STATUS.SLASH_BACK, BODER_STATUS.WIDTH_FRONT} },
            // 二行目
            { 3, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_FRONT, BODER_STATUS.WIDTH_MIDDLE} },
            { 4, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_MIDDLE, BODER_STATUS.SLASH_FRONT, BODER_STATUS.SLASH_BACK, BODER_STATUS.WIDTH_MIDDLE } },
            { 5, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_BACK, BODER_STATUS.WIDTH_MIDDLE } },
            // 三行目
            { 6, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_FRONT, BODER_STATUS.SLASH_BACK, BODER_STATUS.WIDTH_BACK} },
            { 7, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_MIDDLE, BODER_STATUS.WIDTH_BACK } },
            { 8, new List<BODER_STATUS>() { BODER_STATUS.HIGHT_BACK, BODER_STATUS.SLASH_FRONT, BODER_STATUS.WIDTH_BACK } },
        };
    }
}
