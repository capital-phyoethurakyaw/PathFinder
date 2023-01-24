using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PathFinder.gui;
namespace PathFinder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PathFinderBimForm());//
        }

        /*
         * Sequence Setting->
	            SequenceListDatagridview의 순서 변경
	            SequenceListDatagridview의 클릭 방법(더블 클릭)

            RoomGroup Setting -> 
	            RoomListDataGridView의 클릭 방법(더블 클릭) 
	            Order 변경 안되는 문제 ->GridView의 행을 다시 삭제하고 추가

            Analysis 
	            Check 초기 상태 -> Flase
	            Check 변경 시 바로 적용 안되는 문제 해결
	            동선 에러 체크 - 에러 발생 하지 안음
		            N.S ->  4인실 19 -> 4인실 18 -> 4인실 8WAB03 -> 4인실 4 -> 4인실 7 -> N.S
        */
    }
}
