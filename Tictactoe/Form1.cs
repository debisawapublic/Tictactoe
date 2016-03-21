using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tictactoe {
    public partial class Form1: Form {


        List<TTTButton> buttonList = new List<TTTButton>(); //!< ボタンのリスト
        bool turn = true;   //!< 2Pを想定してターンをbooleanで実現

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1 () {
            InitializeComponent();
            // ボタンをプロパティ名から探して、TTTButtonに変換し、リストを作成
            TTTButton.GetControlArrayByName<Button>(this, Data.BUTTON_NAME).ForEach(button => buttonList.Add(new TTTButton(button, button.TabIndex, OnClick)));
            // スタートボタンのイベントをセット
            start.Click += Start_Click;
        }

        /// <summary>
        /// 開始ボタンイベント
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">MouseEventArgs</param>
        private void Start_Click (object sender, EventArgs e) {
            // すべてのボタンのInit関数を呼んで初期化
            buttonList.ForEach(button => button.Init(OnClick));
            richTextBox1.Text = "";
            turn = true;
        }

        /// <summary>
        /// クリックされたときに発動するイベント
        /// </summary>
        /// <param name="button">チクタクトー専用ボタンクラス</param>
        /// <returns>bool</returns>
        public bool OnClick (TTTButton button) {
            // ボタンの属性が変更されていないならばステータスを変更する
            if (button.State == BUTTON_STATE.EMPTY) {
                button.State = turn ? BUTTON_STATE.CIRCLE : BUTTON_STATE.CROSS;
                button.Text = Data.buttonStateSign[button.State];

                // ビンゴかを調べる
                bool bingo = FindBingo();

                // ビンゴだったら終わり
                if (bingo) {
                    richTextBox1.Text = "BINGO!" + Environment.NewLine + "Please, Push \"ゲーム開始\"";
                    buttonList.ForEach(func => func.m_func = null);
                }
                // ターン切り替え（ここ以外でターンを切り替えたい可能性があるかも？
                turn = !turn;
            }
            return true;   
        }

        /// <summary>
        /// ビンゴ状態を探す
        /// </summary>
        /// <returns>boolean</returns>
        private bool FindBingo () {
            List<BODER_STATUS> bingoState = new List<BODER_STATUS>();
            // ターンから取得するステータスを決定し、ビンゴ要件リストを作成
            buttonList.FindAll(state => state.State == (turn ? BUTTON_STATE.CIRCLE : BUTTON_STATE.CROSS)).ForEach(mine => {
                bingoState.AddRange(Data._boderStatusList[mine.Id]);
            });

            bool bingo = false;
            // ボーダーのステータスをすべてチェックし、３つ存在していたらビンゴと判断する
            foreach(BODER_STATUS bs in Enum.GetValues(typeof(BODER_STATUS))) {
                bingo = SearchStateGroup(bingoState, bs);
                // ビンゴだったら止める
                if (bingo)
                    break;
            }
            return bingo;
        }

        /// <summary>
        /// ステータスでグループ化しその数が指定数以上かを調べる
        /// </summary>
        /// <param name="statusList">ビンゴ要件リスト</param>
        /// <param name="i_status">探すステータス</param>
        /// <returns>boolean</returns>
        private bool SearchStateGroup (List<BODER_STATUS> statusList, BODER_STATUS i_status) {
            return statusList.Count(status => status == i_status) >= Data.BINGO_NUMBER;
        }
    }
}
