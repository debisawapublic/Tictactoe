using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tictactoe {
    /// <summary>
    /// ボタンの状態列挙
    /// </summary>
    public enum BUTTON_STATE {
        NONE,
        CIRCLE,
        CROSS,
        EMPTY,
    }

    /// <summary>
    /// チクタクトー専用ボタンクラス
    /// </summary>
    public class TTTButton {
        private Button Button;  //!< ボタン本体

        public Func<TTTButton, bool> m_func { get; set; }   //!< クリックされたときに呼びだすデリゲート関数
        public int Id { get; private set; }                 //!< 自身を示すID

        public string Text {                                //!< ボタンに表示されるテキスト
            set {
                // valueの長さが0以下の場合とボタンがない場合は何もしない
                if(value.Length > 0 && this.Button != null) {
                    Button.Text = value;
                }
            }
        }

        public BUTTON_STATE State { get; set; }             //!< ボタンの状態

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="i_button">ボタン</param>
        /// <param name="i_id">ID</param>
        /// <param name="clickFunc">クリックされた時に発動するdelegate関数</param>
        public TTTButton(Button i_button, int i_id, Func<TTTButton, bool> clickFunc) {
            this.Button = i_button;
            this.Button.Click += Button_Click;
            this.Id = i_id;
            // 初期化
            Init(clickFunc);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init (Func<TTTButton, bool> clickFunc) {
            m_func = clickFunc;
            this.State = BUTTON_STATE.EMPTY;
            this.Text = this.State.ToString();
        }

        /// <summary>
        /// ボタンがクリックされたときのイベント関数
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">MouseEventArgs</param>
        private void Button_Click (object sender, EventArgs e) {
            if(m_func != null)
                // 送る先があれば自身を送る
                m_func(this);
        }

        /// <summary>
        /// 名前から推測されるコントロールの配列を取得する
        /// </summary>
        /// <typeparam name="T">取得したいクラス</typeparam>
        /// <param name="frm">Form</param>
        /// <param name="name">プロパティ名</param>
        /// <returns></returns>
        public static List<T> GetControlArrayByName<T> (Form frm, string name) {
            List<T> ctrs = new List<T>();
            object obj;
            for (int i = 1;
                (obj = FindControlByFieldName(frm, name + i.ToString())) != null;
                i++)
                ctrs.Add((T)obj);
            if (ctrs.Count == 0)
                return null;
            else
                return ctrs;
        }

        /// <summary>
        /// フィールド名からコントロールを見つける
        /// </summary>
        /// <param name="frm">Form</param>
        /// <param name="name">プロパティ名</param>
        /// <returns></returns>
        public static object FindControlByFieldName (Form frm, string name) {
            Type t = frm.GetType();

            System.Reflection.FieldInfo fi = t.GetField(
                name,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly);

            if (fi == null)
                return null;

            return fi.GetValue(frm);
        }
    }
}
