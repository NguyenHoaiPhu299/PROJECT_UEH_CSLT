using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROJECT_UEH_CSLT
{
    internal class Program
    {
        #region Variable
        #region Variable for Data Structures
        static List<string> ListName;  // List chứa tất cả tên của người chơi (khác nhau)
        static int Space = 0;// Chiều dài str (chuỗi trên 1 dòng)
        #endregion
        #region
        /*static int level = 1;*/
        #endregion

        #region Variable for main box
        // Làm bảng không bị lỗi kí tự nên sử dụng hai biến này
        static int boxColumn = 30;
        static int boxRow = 120;
        #endregion

        #region Variable for Player 1 and Player 2
        static int firstPlayerPadSize = 10; // Chiều dài thanh 1
        static int AIPlayerPadSize = 4; // Chiều dài thanh AI
        static string ten; // Tên người chơi
        static int firstPlayerPosition = 1; // Vị trí của player 1
        static int AIPlayerPosition = 1; // Vị trí của player AI
        #endregion

        #region Variable for Ball
        static int ball1PositionX = 3; // Tọa độ X của quả bóng
        static int ball1PositionY = 1; // Tọa độ Y của quả bóng
        static bool ball1DirectionUp = true; // Xác định hướng đi lên/ xuống của quả bóng 1
        static bool ball1DirectionRight = false; // Xác định hướng đi qua trái/ phải của quả bóng 1
        // Tương tự cho bóng 2
        static int ball2PositionX = 3;
        static int ball2PositionY = 1;
        static bool ball2DirectionUp = false;
        static bool ball2DirectionRight = true;
        #endregion

        #region Variable for result
        static int firstPlayerResult = 18; // Điểm số của bạn
        /*static int AIPlayerResult = 0;*/
        #endregion

        #region Random num
        static Random randomGenerator = new Random(); // Dành cho logic AI
        #endregion
        static int speed = 80; // Tốc độ của quả bóng
        static int prob = 70; // Xác suất sử dụng cho logic của AI

        #region shop
        static int yourChoiceX = 2;
        static int yourChoiceY = 2;

        static int choice1_1PositionX = 2, choice1_1PositionY = 2; static bool checkChoice1_1 = false;
        static int choice1_2PositionX = 28, choice1_2PositionY = 2; static bool checkChoice1_2 = false;
        static int choice1_3PositionX = 54, choice1_3PositionY = 2; static bool checkChoice1_3 = false;
        static int choice2_1PositionX = 2, choice2_1PositionY = 11; static bool checkChoice2_1 = false;
        static int choice2_2PositionX = 28, choice2_2PositionY = 11; static bool checkChoice2_2 = false;
        static int choice2_3PositionX = 54, choice2_3PositionY = 11; static bool checkChoice2_3 = false;
        static int choice3_1PositionX = 2, choice3_1PositionY = 20; static bool checkChoice3_1 = false;
        static int choice3_2PositionX = 28, choice3_2PositionY = 20; static bool checkChoice3_2 = false;
        static int choice3_3PositionX = 54, choice3_3PositionY = 20; static bool checkChoice3_3 = false;

        static string[,] s = new string[,] {
            { "+2 heart", "+2 Chiều dài thanh", "+3 Chiều dài thanh" },
            { "+40 Speed", "+60 Speed", "+100 Speed" },
            { "-5% độ chính xác AI", "-10% độ chính xác AI", "-20% độ chính xác AI" }
        };

        static string[,] t = new string[,] {
            { "100 vàng", "23 vàng", "26 vàng" },
            { "23 vàng", "27 vàng", "30 vàng" },
            { "25 vàng", "28 vàng", "35 vàng" }
        };
        static int money = 100;
        static int moneyChoice1_1 = 100;
        static int moneyChoice1_2 = 23;
        static int moneyChoice1_3 = 26;
        static int moneyChoice2_1 = 23;
        static int moneyChoice2_2 = 27;
        static int moneyChoice2_3 = 30;
        static int moneyChoice3_1 = 25;
        static int moneyChoice3_2 = 28;
        static int moneyChoice3_3 = 35;
        static int posX = 2;
        static int posY = 5;
        #endregion

        static int heart = 5;
        #region Rank
        static string player;
        #endregion
        #endregion

        // BEGIN FUNCTION
        #region FUNCTION

        // Chuỗi hàm hỗ trợ
        #region support function
        static void RemoveBoard() //Hàm xóa bảng
        {
            int y = 17;
            RemoveObject(120 * 4 / 6 + 8, y, Space);
            y += 2;
            RemoveObject(120 * 4 / 6 + 8, y, Space);
            y += 2;
            RemoveObject(120 * 4 / 6 + 8, y, Space);
            y += 2;
            RemoveObject(120 * 4 / 6 + 8, y, Space);
            y += 2;
            RemoveObject(120 * 4 / 6 + 8, y, Space);
        }
        static int convert(string s) //Đổi chuỗi số thành dạng số (int, long)
        {
            int giatri = 0;
            for (int i = 0; i < s.Length; i++)
            {
                giatri = giatri * 10 + (s[i] - '0');
            }
            return giatri;
        }
        static bool CheckName(string s) //Hàm Kiểm tra xem chuỗi nhập vào là chuỗi số hay chuỗi ký tự
        {
            int dem = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) == true)
                {
                    ++dem;
                }
            }
            if (dem == s.Length) return false;
            else
            {
                return true;
            }
        }
        static void ReadBoard(string ten, string diem) //Hàm thêm dữ liệu vào Notepad
        {
            FileStream fs = new FileStream("output.txt", FileMode.Append, FileAccess.Write);
            StreamWriter swriter = new StreamWriter(fs);
            swriter.Write(ten + "                       " + diem + "\n");
            swriter.Flush();
            fs.Close();
        }
        static void RemoveScreenGameField()
        {
            for (int i = 1; i <= 28; i++) // Reset màn hình
                RemoveObject(1, i, boxRow / 3 * 2 - 2);
        }
        static void DrawBall(int ballX, int ballY) // Dùng để vẽ quả bóng
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintAtPosition(ballX, ballY, 'Ⓞ');
            Console.ResetColor();
        }
        static void RemoveScrollBars() // Xóa scrollbars đi
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }
        static void PrintAtPosition(int x, int y, char Symbol) // Làm hàm giúp in một kí tự bất kì ra
        {
            Console.SetCursorPosition(x, y);
            Console.Write(Symbol);
        }
        /*static void PrintAtPositionColor(int x, int y, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
        }*/
        static void PrintPlayerPoint() //Hàm in ra tên và điểm người chơi phía dưới bảng xếp hạng
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(120 * 4 / 6 + 8, 28);
            Console.WriteLine(ten);
            Console.SetCursorPosition(120 * 4 / 6 + 30, 28);
            Console.WriteLine(firstPlayerResult);
        }
        static void RemoveObject(int x, int y, int N) // Dùng để xóa vết của những đối tượng di chuyển cần xóa
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', N));
            Console.SetCursorPosition(x, y);
        }
        static void DrawMainBox() // Vẽ khung chính, là khung ngoài cùng
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintAtPosition(0, 0, '╔');
            PrintAtPosition(boxRow - 1, 0, '╗');
            PrintAtPosition(0, boxColumn - 1, '╚');
            PrintAtPosition(boxRow - 1, boxColumn - 1, '╝');
            
            for (int i = 1; i < boxColumn - 1; i++) // Left
                PrintAtPosition(0, i, '║');
            for (int i = 1; i < boxColumn - 1; i++) // Right
                PrintAtPosition(boxRow - 1, i, '║');
            for (int i = 1; i < boxRow - 1; i++) // Top
                PrintAtPosition(i, 0, '═');
            for (int i = 1; i < boxRow - 1; i++) // Bottom
                PrintAtPosition(i, boxColumn - 1, '═');
            // Divide
            PrintAtPosition(boxRow / 3 * 2 - 1, 0, '╦');
            PrintAtPosition(boxRow / 3 * 2 - 1, boxColumn - 1, '╩');
            for (int i = 1; i < boxColumn - 1; i++)
                PrintAtPosition(boxRow / 3 * 2 - 1, i, '║');
            // Divide Board and Player Points
            PrintAtPosition(boxRow - 1, 27, '╣');
            PrintAtPosition(boxColumn + 7 * 7, 27, '╠');
            for (int i = 2; i < boxColumn + 11; i++)
                PrintAtPosition(boxRow - i, 27, '═');
        }
        #endregion // END chuỗi hàm hỗ trợ

        // Chuỗi hàm chức năng phụ
        #region Secondary function
        static void PrintBoard() //HHàm in ra TOP 5 từ Notepad
        {
            FileStream fs = new FileStream("output.txt", FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(fs);
            int linesPrinted = 1;
            int k = 17;
            while (linesPrinted < 6 && !sread.EndOfStream) //In ra 5 thí sinh cao điểm nhất
            {
                string str = sread.ReadLine();
                if (!string.IsNullOrEmpty(str))
                {
                    Space = str.Length;
                    string Name = "";
                    string Diem = "";
                    string[] parts = str.Split(' ');

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (CheckName(parts[i]))
                        {
                            Name += parts[i] + " "; // Tách phần tên để in riêng
                        }
                        else
                        {
                            Diem += parts[i] + " "; // Tách phần điểm để in riêng
                        }
                    }
                    Name = Name.Trim(); //Xóa sạch khoảng trống 2 đầu
                    Diem = Diem.Trim(); //Xóa sạch khoảng trống 2 đầu
                    if (Name == ten) //Hightlight tên của người chơi đang chơi nếu điểm ở trong top 5
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(120 * 4 / 6 + 8, k);
                        Console.WriteLine(linesPrinted + ".     " + Name);
                        Console.SetCursorPosition(120 * 4 / 6 + 30, k);
                        Console.WriteLine(Diem);
                    }
                    else //Màu của các người chơi khác (trừ người đang chơi)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(120 * 4 / 6 + 8, k);
                        Console.WriteLine(linesPrinted + ".     " + Name);
                        Console.SetCursorPosition(120 * 4 / 6 + 30, k);
                        Console.WriteLine(Diem);
                    }
                    Console.ResetColor();
                }
                k += 2;
                linesPrinted++;
            }
            fs.Close();
        }
        static void TakeBoard() //Hàm xử lý và sắp xếp dữ liệu đầu vào Notepad
        {
            HashSet<Tuple<string, int>> DanhSach = new HashSet<Tuple<string, int>>(); //Khai báo 1 Cấu trúc dữ liệu để lưu trữ bảng xếp hạng
            List<string> list = new List<string>(); //1 List để 1 lát add HashSet ở trên để dễ sort
            using (FileStream fs = new FileStream("output.txt", FileMode.Open, FileAccess.Read)) //Hàm đọc find thông thường
            using (StreamReader sread = new StreamReader(fs))
            {
                while (!sread.EndOfStream) //Đọc đến cuối của NotePad
                {
                    string str = sread.ReadLine();
                    if (!string.IsNullOrEmpty(str)) //Liệu dòng đó có rỗng hay không
                    {
                        string Name = "";
                        string Diem = "";
                        string[] parts = str.Split(' '); //Tách tên ra để lọc phần điểm và phần tên

                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (CheckName(parts[i]))
                            {
                                Name += parts[i] + " "; //Tách phần tên ra 1 biến
                            }
                            else
                            {
                                Diem += parts[i] + " "; //Tách phần điểm ra 1 biến
                            }
                        }
                        Name = Name.Trim(); //Xóa khoảng trống 2 bên
                        Diem = Diem.Trim(); //Xóa khoảng trống 2 bên
                        int diemValue = convert(Diem); //Chuyển chuỗi Diem thành điểm số

                        Tuple<string, int> existingTuple = DanhSach.FirstOrDefault(t => t.Item1 == Name); //Tìm ra cặp <string, int> đầu tiên có phần Item1 trùng với phần Name (tên)

                        if (existingTuple != null && diemValue > existingTuple.Item2 || existingTuple == null)//Nếu chưa có trong DanhSach thì add vào còn đã có thì phải xét thêm trường hợp riêng
                        {
                            DanhSach.Remove(existingTuple); //Nếu đã tồn tại tên đó và số điểm lớn hơn số điểm đã chứa thì xóa đi cặp cũ
                            DanhSach.Add(Tuple.Create(Name, diemValue)); // Add thêm cặp mới với số điểm lớn hơn vào
                        }


                    }
                }
            }
            ListName = DanhSach.Select(tuple => tuple.Item1).ToList(); // Add phần DanhSach(HashSet) đã được lọc ở trên và lấy ra phần string và đưa vào 1 List mới để làm việc khác (phần hàm nhập tên để không nhập trùng)
            List<Tuple<string, int>> DanhSachList = DanhSach.ToList(); // Add phần DanhSach(HashSet) đã được lọc ở trên vào 1 list (để sử dụng sort)
            DanhSachList.Sort((tuple1, tuple2) => tuple2.Item2.CompareTo(tuple1.Item2)); // Sắp xếp so sánh từ lớn tới nhỏ của phần Item2 (Điểm số người chơi)
            using (FileStream ts = new FileStream("output.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter swriter = new StreamWriter(ts))
            {
                foreach (var kvp in DanhSachList)
                {
                    swriter.WriteLine((kvp.Item1 + "             " + kvp.Item2)); // Thêm phần DanhSachList vào trong NotePad và lưu lại để in bxh
                }
                swriter.Flush();
            }
        }
        static void StartPage() // Màn hình bắt đầu
        {
            Console.SetCursorPosition(boxRow / 3 - 18, boxColumn / 2 - 2);
            Console.WriteLine("CHÀO MỪNG BẠN ĐẾN VỚI GAME PINGPONG !");
            Console.SetCursorPosition(boxRow / 3 - 8, boxColumn / 2 - 1);
            Console.WriteLine("Thể lệ trò chơi: ");
            Console.SetCursorPosition(boxRow / 3 - 11, boxColumn / 2);
            Console.WriteLine("Biệt danh của bạn là: ");
            while (true)
            {
                string ErrorLongName = "Tên quá dài, xin vui lòng nhập lại!";
                string ErrorNumName = "Tên không được chỉ bao gồm số, xin vui lòng nhập lại!";
                string ErrorExistName = "Tên đã tồn tại, xin vui lòng đặt tên khác!";
                Console.SetCursorPosition(boxRow / 3 - 11, boxColumn / 2 + 1);
                ten = Console.ReadLine();
                TakeBoard(); // Hàm cập nhật danh sách tên cho ListName
                RemoveObject(4, boxColumn - 3, ErrorNumName.Length + 15);
                if (CheckName(ten) == false || ten.Length > 13 || ListName.Find(s => s == ten) != null)
                {

                    if (CheckName(ten) == false) //Nếu tên chỉ toàn là số
                    {
                        Console.SetCursorPosition(boxRow / 3 - 26, boxColumn - 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ErrorNumName);
                        Console.ResetColor();
                        RemoveObject(boxRow / 3 - 11, boxColumn / 2 + 1, ten.Length);
                        continue;
                    }
                    else if (ten.Length > 13) //Nếu tên quá dài (dài hơn 13 ký tự)
                    {
                        Console.SetCursorPosition(boxRow / 3 - 17, boxColumn - 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ErrorLongName);
                        Console.ResetColor();
                        RemoveObject(boxRow / 3 - 11, boxColumn / 2 + 1, ten.Length);
                        continue;
                    }
                    else //Nếu tên đã tồn tại sẵn rồi
                    {
                        Console.SetCursorPosition(boxRow / 3 - 20, boxColumn - 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ErrorExistName);
                        Console.ResetColor();
                        RemoveObject(boxRow / 3 - 11, boxColumn / 2 + 1, ten.Length);
                        continue;
                    }
                }
                else
                {
                    break; //Nhập đúng thì break
                }
            }
            string point = firstPlayerResult.ToString();
            ReadBoard(ten, point);
            TakeBoard();
            Console.SetCursorPosition(boxRow / 3 - 21, boxColumn / 2 + 2);
            Console.WriteLine("Nhấn một phím bất kỳ để bắt đầu trò chơi !");
            Console.ReadKey(true);
            Thread.Sleep(1000);
            RemoveObject(boxRow / 3 - 18, boxColumn / 2 - 2, 37);
            RemoveObject(boxRow / 3 - 8, boxColumn / 2 - 1, 17);
            RemoveObject(boxRow / 3 - 11, boxColumn / 2, 22);
            RemoveObject(boxRow / 3 - 11, boxColumn / 2 + 1, ten.Length);
            RemoveObject(boxRow / 3 - 21, boxColumn / 2 + 2, 42);


        }
        static void PauseFunction() // Dùng để dừng chương trình
        {
            while (true) // Dùng để tạm dừng chương trình
            {
                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Q) // Khi bấm Q thì hủy vòng lặp và chạy chương trình
                        break;
                }
            }
        }
        static void SetInitialPositions() // Cài đặt vị trí ban đầu của tất cả đối tượng
        {
            firstPlayerPosition = boxColumn / 2 - firstPlayerPadSize / 2; // Thanh player 1 nằm giữa chiều dọc
            AIPlayerPosition = boxColumn / 2 - AIPlayerPadSize / 2; // Thanh player AI nằm giữa chiều dọc
            SetBall1AtTheMiddleOfTheGameField(); // Quả bóng 1 nằm giữa trường chơi
        }
        #endregion // END chuỗi hàm chức năng phụ

        // Hàm sử dụng cho ball
        #region Ball
        #region Ball 1
        static void SetBall1AtTheMiddleOfTheGameField() // Cài đặt vị trí ban đầu của bóng 1 nằm giữa trường chơi
        {
            ball1PositionX = boxRow / 3 - 1;
            ball1PositionY = boxColumn / 2 - 1;
        }
        static void RemoveBall1() // Dùng để xóa vết của quả bóng 1 để lại khi nó chạy
        {
            if (ball1DirectionRight == false && ball1DirectionUp == true)
                RemoveObject(ball1PositionX + 1, ball1PositionY + 1, 1);
            if (ball1DirectionRight == false && ball1DirectionUp == false)
                RemoveObject(ball1PositionX + 1, ball1PositionY - 1, 1);
            if (ball1DirectionRight == true && ball1DirectionUp == false)
                RemoveObject(ball1PositionX - 1, ball1PositionY - 1, 1);
            if (ball1DirectionRight == true && ball1DirectionUp == true)
                RemoveObject(ball1PositionX - 1, ball1PositionY + 1, 1);
        }
        private static void MoveBall1()
        {

            if (ball1PositionY == 1) // Nếu quả bóng ở trên cùng thì nó sẽ nảy xuống
                ball1DirectionUp = false;
            if (ball1PositionY == boxColumn - 2)  // Nếu quả bóng ở dưới cùng thì nó sẽ nảy lên
                ball1DirectionUp = true;
            // Truong hop win
            if (ball1PositionX == boxRow / 3 * 2 - 2) // Nếu quả bóng vượt qua thanh dọc của AI thì player 1 thắng
            {
                RemoveObject(ball1PositionX, ball1PositionY, 1);
                SetBall1AtTheMiddleOfTheGameField(); // Cài lại vị trí ban đầu của quả bóng 1
                ball1DirectionRight = false; // Cài lại hướng di chuyển lên xuống ban đầu của quả bóng
                ball1DirectionUp = true; // Cài lại hướng di chuyển trái phải ban đầu của quả bóng
                firstPlayerResult++;
                money += 2;
                ReadBoard(ten, firstPlayerResult.ToString());
                TakeBoard();
                RemoveBoard();
                PrintBoard();
                Console.SetCursorPosition(boxRow / 3 - 1 - 9, boxColumn / 2 - 1);
                Console.WriteLine("First player wins !");
                Console.ReadKey(true);
                RemoveObject(boxRow / 3 - 1 - 9, boxColumn / 2 - 1, 19);
                ResetForLevel();
                if (firstPlayerResult <= 9)
                    Level1();
                if (firstPlayerResult >= 10 && firstPlayerResult <= 19)
                    Level2();
            }
            if (ball1PositionX == 1) // Nếu quả bóng vượt qua thanh dọc của player 1 thì AI thắng
            {
                RemoveObject(ball1PositionX, ball1PositionY, 1);
                SetBall1AtTheMiddleOfTheGameField();
                ball1DirectionRight = true;
                ball1DirectionUp = true;
                heart--;
                Console.SetCursorPosition(boxRow / 3 - 1 - 9, boxColumn / 2 - 1);
                Console.WriteLine("Second player wins !");
                Console.ReadKey(true);
                RemoveObject(boxRow / 3 - 1 - 9, boxColumn / 2 - 1, 20);
            }
            // Dùng để cài đặt quả bóng sẽ nảy khi đập vào thanh dọc
            if (ball1PositionX < 4)
            {
                if (ball1PositionY >= firstPlayerPosition && ball1PositionY < firstPlayerPosition + firstPlayerPadSize)
                {
                    ball1DirectionRight = true;
                    money++;
                }
            }
            if (ball1PositionX >= boxRow / 3 * 2 - 4)
                if (ball1PositionY >= AIPlayerPosition && ball1PositionY < AIPlayerPosition + AIPlayerPadSize)
                    ball1DirectionRight = false;
            // Dùng để di chuyển quả bóng
            if (ball1DirectionUp)
                ball1PositionY--;
            else
                ball1PositionY++;

            if (ball1DirectionRight)
                ball1PositionX++;
            else
                ball1PositionX--;
        }
        #endregion // End ball 1
        #region Ball 2
        static void SetBall2AtTheMiddleOfTheGameField() // Cài đặt vị trí ban đầu của bóng 2 nằm giữa trường chơi
        {
            ball2PositionX = boxRow / 3 - 1;
            ball2PositionY = boxColumn / 2 - 1;
        }
        static void RemoveBall2() // Dùng để xóa vết của quả bóng 2 để lại khi nó chạy
        {
            if (ball2DirectionRight == false && ball2DirectionUp == true)
                RemoveObject(ball2PositionX + 1, ball2PositionY + 1, 1);
            if (ball2DirectionRight == false && ball2DirectionUp == false)
                RemoveObject(ball2PositionX + 1, ball2PositionY - 1, 1);
            if (ball2DirectionRight == true && ball2DirectionUp == false)
                RemoveObject(ball2PositionX - 1, ball2PositionY - 1, 1);
            if (ball2DirectionRight == true && ball2DirectionUp == true)
                RemoveObject(ball2PositionX - 1, ball2PositionY + 1, 1);
        }
        private static void MoveBall2() // Logic di chuyển giống với quả bóng 1
        {
            if (ball2PositionY == 1)
                ball2DirectionUp = false;
            if (ball2PositionY == boxColumn - 2)
                ball2DirectionUp = true;
            // Truong hop win
            if (ball2PositionX == boxRow / 3 * 2 - 2)
            {
                RemoveObject(ball2PositionX, ball2PositionY, 1);
                SetBall2AtTheMiddleOfTheGameField();
                ball2DirectionRight = false;
                ball2DirectionUp = true;
                firstPlayerResult++;
                money += 2;
                ReadBoard(ten, firstPlayerResult.ToString());
                TakeBoard();
                RemoveBoard();
                PrintBoard();
                Console.SetCursorPosition(boxRow / 3 - 1 - 9, boxColumn / 2 - 1);
                Console.WriteLine("First player wins !");
                Console.ReadKey(true);
                RemoveObject(boxRow / 3 - 1 - 9, boxColumn / 2 - 1, 19);
                ResetForLevel();
                if (firstPlayerResult <= 4)
                    Level1();
                if (firstPlayerResult >= 7 && firstPlayerResult <= 10)
                    Level2();
            }
            if (ball2PositionX == 1)
            {
                RemoveObject(ball2PositionX, ball2PositionY, 1);
                SetBall2AtTheMiddleOfTheGameField();
                ball2DirectionUp = true;
                ball2DirectionRight = true;
                heart--;
                Console.SetCursorPosition(boxRow / 3 - 1 - 9, boxColumn / 2 - 1);
                Console.WriteLine("Second player wins !");
                Console.ReadKey(true);
                RemoveObject(boxRow / 3 - 1 - 9, boxColumn / 2 - 1, 20);
            }
            // Truong hop nay tren thanh doc
            if (ball2PositionX < 4)
            {
                if (ball2PositionY >= firstPlayerPosition && ball2PositionY < firstPlayerPosition + firstPlayerPadSize)
                {
                    ball2DirectionRight = true;
                    money++;
                }
            }
            if (ball2PositionX >= boxRow / 3 * 2 - 4)
                if (ball2PositionY >= AIPlayerPosition && ball2PositionY < AIPlayerPosition + AIPlayerPadSize)
                    ball2DirectionRight = false;

            // Di chuyen
            if (ball2DirectionUp)
                ball2PositionY--;
            else
                ball2PositionY++;

            if (ball2DirectionRight)
                ball2PositionX++;
            else
                ball2PositionX--;
        }
        #endregion // ENd ball 2
        #endregion // End chuỗi hàm sử dụng cho ball

        // Chuỗi hàm sử dụng cho player 1
        #region Player 1
        static void DrawFirstPlayer() // Vẽ player 1
        {
            for (int i = firstPlayerPosition; i < firstPlayerPosition + firstPlayerPadSize; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintAtPosition(1, i, '│');
                PrintAtPosition(2, i, '▐');
                Console.ResetColor();
            }
        }
        static void MoveFirstPlayerDown() // Dùng để di chuyển player 1 xuống dưới
        {
            if (firstPlayerPosition < boxColumn - firstPlayerPadSize - 1)
                firstPlayerPosition++;
        }
        static void MoveFirstPlayerUp() // Dùng để di chuyển player 1 lên trên
        {
            if (firstPlayerPosition > 1)
                firstPlayerPosition--;
        }
        static void RemovePlayer1(ConsoleKeyInfo keyInfo) // Dùng để xóa vết cũ của thanh dọc lúc di chuyển
        {
            if (keyInfo.Key == ConsoleKey.UpArrow)
                for (int i = firstPlayerPosition + firstPlayerPadSize; i > firstPlayerPosition; i--) // Dùng để xóa vết cũ của thanh dọc lúc di chuyển lên trên
                    RemoveObject(1, i, 2);
            if (keyInfo.Key == ConsoleKey.DownArrow)
                for (int i = firstPlayerPosition - 1; i < firstPlayerPosition + firstPlayerPadSize; i++) // Dùng để xóa vết cũ của thanh dọc lúc di chuyển xuống dưới
                    RemoveObject(1, i, 2);
        }
        #endregion

        // Chuỗi hàm sử dụng cho AI
        #region AI
        static void DrawAIPlayer() // Vẽ player AI
        {
            for (int i = AIPlayerPosition; i < AIPlayerPosition + AIPlayerPadSize; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintAtPosition(boxRow / 3 * 2 - 2, i, '│');
                PrintAtPosition(boxRow / 3 * 2 - 3, i, '▌');
                Console.ResetColor();
            }
        }
        static void MoveAIPlayerDown() // Dùng để di chuyển player AI xuống dưới
        {
            if (AIPlayerPosition < boxColumn - AIPlayerPadSize - 1)
                AIPlayerPosition++;
        }
        static void MoveAIPlayerUp() // Dùng để di chuyển player AI lên trên
        {
            if (AIPlayerPosition > 1)
                AIPlayerPosition--;
        }
        static void AIPlayerAIMove() // Logic của thanh dọc AI
        {
            int randomNumber = randomGenerator.Next(1, 101);
            if (randomNumber < prob) // Xác suất để thanh dọc AI chạy theo quả banh 1 theo trục Y
            {
                if (firstPlayerResult < 15) // Thanh dọc AI sẽ chạy theo quả banh 1 theo trục Y (Level 1 và level 2)
                {
                    // Thanh dọc AI sẽ di chuyển theo hướng di chuyển lên xuống của quả bóng thứ nhất
                    if (ball1DirectionUp == true)
                    {
                        MoveAIPlayerUp();
                        for (int i = AIPlayerPosition + AIPlayerPadSize; i > AIPlayerPosition; i--)
                            RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                    }
                    else
                    {
                        MoveAIPlayerDown();
                        for (int i = AIPlayerPosition - 1; i < AIPlayerPosition + AIPlayerPadSize; i++)
                            RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                    }
                }
                else // Thanh dọc AI sẽ chạy theo quả banh theo trục Y (Level 3)
                {
                    if (ball1DirectionRight == true && ball2DirectionRight == false
                        || ball1DirectionRight == true && ball2DirectionRight == true && ball1PositionX > ball2PositionX) // Trường hợp thanh dọc AI sẽ đuổi theo quả bóng thứ nhất theo trục Y
                    {
                        // Trong trường hợp bóng 1 qua phải, bóng 2 qua trái hoặc
                        // Trong trường hợp cả hai bóng cùng qua phải nhưng vị trí của bóng 1 gần thanh dọc AI hơn
                        if (ball1DirectionUp == true)
                        {
                            MoveAIPlayerUp();
                            for (int i = AIPlayerPosition + AIPlayerPadSize; i > AIPlayerPosition; i--)
                                RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                        }
                        else
                        {
                            MoveAIPlayerDown();
                            for (int i = AIPlayerPosition - 1; i < AIPlayerPosition + AIPlayerPadSize; i++)
                                RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                        }
                    }
                    if (ball2DirectionRight == true && ball1DirectionRight == false
                        || ball2DirectionRight == true && ball1DirectionRight == true && ball2PositionX > ball1PositionX)  // Trường hợp thanh dọc AI sẽ đuổi theo quả bóng thứ hai theo trục Y
                    {
                        // Trong trường hợp bóng 2 qua phải, bóng 1 qua trái hoặc
                        // Trong trường hợp cả hai bóng cùng qua phải nhưng vị trí của bóng 2 gần thanh dọc AI hơn
                        if (ball2DirectionUp == true)
                        {
                            MoveAIPlayerUp();
                            for (int i = AIPlayerPosition + AIPlayerPadSize; i > AIPlayerPosition; i--)
                                RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                        }
                        else
                        {
                            MoveAIPlayerDown();
                            for (int i = AIPlayerPosition - 1; i < AIPlayerPosition + AIPlayerPadSize; i++)
                                RemoveObject(boxRow / 3 * 2 - 3, i, 2);
                        }
                    }
                }
            }
        }
        #endregion

        // Chuỗi hàm cho level
        #region Level
        static void DrawLevel(int level) // Dùng để in ra level bao nhiêu
        {
            Console.SetCursorPosition(boxRow / 3 - 7 / 2, boxColumn / 2);
            Console.Write("Level " + level);
            Thread.Sleep(2000);
            Console.SetCursorPosition(boxRow / 3 - 7 / 2, boxColumn / 2);
            Console.Write(new string(' ', 7));
            Console.SetCursorPosition(boxRow / 3 - 7 / 2, boxColumn / 2);
        }
        static void ResetForLevel() // Dùng để reset màn hình và in ra bạn đã tới level nào
        {
            RemoveScreenGameField();
            switch (firstPlayerResult) // In ra bạn tới level nào
            {
                case 10:
                    DrawLevel(2); break;
                case 20:
                    DrawLevel(3); break;
            }
        }
        static void Level1() // Level 1
        {
            if (firstPlayerResult <= 9)
            {
                if (firstPlayerResult % 2 == 1)
                {
                    firstPlayerPadSize--;
                    speed -= 4;
                    prob += 2;
                }
                if (firstPlayerResult % 2 == 0 && firstPlayerResult != 0)
                {
                    speed -= 3;
                    prob += 1;
                }
            }
            RemoveObject(1, firstPlayerPosition + firstPlayerPadSize, 2);
        }
        static void Level2() // Level 2
        {
            if (firstPlayerResult >= 10 && firstPlayerResult <= 19)
            {
                if (firstPlayerResult % 2 == 1)
                {
                    if (AIPlayerPadSize + AIPlayerPosition > 28) // Trong trường hợp thanh dọc nằm dưới cùng thì khi cộng chiều dài, vị trí sẽ giảm theo để không bị tràn ra khung
                        AIPlayerPosition -= 2;
                    AIPlayerPadSize += 2;
                    prob += 2;
                }
                else  speed -= 5;
            }
        }
        static void Level3() // Level 3
        {   
            if (firstPlayerResult == 20)
            {
                prob = 100;
            }
            MoveBall2(); // Dịch bóng 2 sang  vị trí mới
            RemoveBall2(); // Xóa vết cũ của bóng 2
            DrawBall(ball2PositionX, ball2PositionY); // In ra bóng 2 tại vị trí mới
        }
        #endregion

        // Chuỗi hàm cho shop
        #region Shop
        static void NotEnough()
        {
            RemoveObject(boxRow / 3 * 2, 20, boxRow / 3 - 1);
            RemoveObject(boxRow / 3 * 2, 21, boxRow / 3 - 1);
            Console.SetCursorPosition(boxRow * 5 / 6 - 10, 8);
            Console.WriteLine("Bạn Không đủ tiền mua");
            Console.SetCursorPosition(boxRow * 5 / 6 - 15, 9);
            Console.WriteLine("Bạn hãy chọn một sản phẩm khác");
        }
        static void BuySuccess()
        {
            RemoveObject(boxRow / 3 * 2, 20, boxRow / 3 - 1);
            RemoveObject(boxRow / 3 * 2, 21, boxRow / 3 - 1);
            Console.SetCursorPosition(boxRow * 5 / 6 - 10, 8);
            Console.WriteLine("Bạn đã mua thành công");
            Console.SetCursorPosition(boxRow * 5 / 6 - 17, 9);
            Console.WriteLine("Bạn không thể mua lại vật phẩm này");
        }
        static void Bought()
        {
            RemoveObject(boxRow / 3 * 2, 20, boxRow / 3 - 1);
            RemoveObject(boxRow / 3 * 2, 21, boxRow / 3 - 1);
            Console.SetCursorPosition(boxRow * 5 / 6 - 11, 8);
            Console.WriteLine("Bạn đã mua sản phẩm này");
            Console.SetCursorPosition(boxRow * 5 / 6 - 10, 9);
            Console.WriteLine("Bạn không thể mua lại");
        }
        static void Box(int x, int y) // Dùng để vẽ khung các sản phẩm trong shop
        {
            if (yourChoiceX == x && yourChoiceY == y)
                Console.ForegroundColor = ConsoleColor.Red;
            PrintAtPosition(x, y, '╔');
            PrintAtPosition(x + 23, y, '╗');
            PrintAtPosition(x, y + 6, '╚');
            PrintAtPosition(x + 23, y + 6, '╝');
            for (int i = x + 1; i < x + 23; i++)
                PrintAtPosition(i, y, '═');
            for (int i = x + 1; i < x + 23; i++)
                PrintAtPosition(i, y + 6, '═');
            for (int i = y + 1; i < y + 6; i++)
                PrintAtPosition(x, i, '║');
            for (int i = y + 1; i < y + 6; i++)
                PrintAtPosition(x + 23, i, '║');
            Console.ResetColor();
        }
        static void ShopContent(int x, int y) // Tên sản phẩm sẽ mua
        {

            for (int i = 0; i < 3; i++)
            {
                int a = x, b = y;
                for (int j = 0; j < 3; j++)
                {
                    if (i == 0)
                    {
                        Console.SetCursorPosition(a + 3, b);
                        Console.Write(s[i, j]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(a + 4, b + 4);
                        Console.Write(t[i, j]);
                        Console.ResetColor();
                    }
                    if (i == 1)
                    {
                        Console.SetCursorPosition(a + 7, b);
                        Console.Write(s[i, j]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(a + 4, b + 4);
                        Console.Write(t[i, j]);
                        Console.ResetColor();
                    }
                    if (i == 2)
                    {
                        Console.SetCursorPosition(a + 2, b);
                        Console.Write(s[i, j]);
                        Console.SetCursorPosition(a + 5, b + 4);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(t[i, j]);
                        Console.ResetColor();
                    }
                    a += 26;
                }
                Console.WriteLine();
                y += 9;
            }
        }
        static void Shop() // In ra giao diện shop
        {
            Console.SetCursorPosition(boxRow / 3 * 2 + 4, 3);
            Console.WriteLine("money: " + money);
            Console.SetCursorPosition(boxRow / 3 * 2 + 4, 5);
            Console.WriteLine("heart: " + heart);
            Box(choice1_1PositionX, choice1_1PositionY);
            Box(choice1_2PositionX, choice1_2PositionY);
            Box(choice1_3PositionX, choice1_3PositionY);
            Box(choice2_1PositionX, choice2_1PositionY);
            Box(choice2_2PositionX, choice2_2PositionY);
            Box(choice2_3PositionX, choice2_3PositionY);
            Box(choice3_1PositionX, choice3_1PositionY);
            Box(choice3_2PositionX, choice3_2PositionY);
            Box(choice3_3PositionX, choice3_3PositionY);
            ShopContent(posX, posY);
        }
        static void MainShop()
        {
            for (int i = 1; i <= 28; i++)
                RemoveObject(1, i, boxRow / 3 * 2 - 2);
            while (true)
            {
                Shop();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (yourChoiceY <= 20 && yourChoiceY >= 11)
                        {
                            yourChoiceY -= 9;
                            Shop();
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (yourChoiceY <= 11 && yourChoiceY >= 2)
                        {
                            yourChoiceY += 9;
                            Shop();
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        if (yourChoiceX <= 28 && yourChoiceX >= 2)
                        {
                            yourChoiceX += 26;
                            Shop();
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        if (yourChoiceX <= 54 && yourChoiceX >= 28)
                        {
                            yourChoiceX -= 26;
                            Shop();
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.W) // Dùng để tắt shop
                    {
                        for (int i = 1; i <= 28; i++)
                        {
                            RemoveObject(1, i, boxRow / 3 * 2 - 2);
                        }
                        for (int i = 1; i <= 26; i++)
                        {
                            RemoveObject(boxRow / 3 * 2, i, boxRow / 3 - 2);
                        }
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        if (checkChoice1_1 == false)
                        {
                            if (yourChoiceX == choice1_1PositionX && yourChoiceY == choice1_1PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice1_1)
                                {
                                    heart += 2;
                                    checkChoice1_1 = true;
                                    BuySuccess();
                                    money -= moneyChoice1_1;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice1_1PositionX && yourChoiceY == choice1_1PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice1_2 == false)
                        {
                            if (yourChoiceX == choice1_2PositionX && yourChoiceY == choice1_2PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice1_2)
                                {
                                    if (firstPlayerPadSize + firstPlayerPosition > 28)
                                    {
                                        firstPlayerPosition -= 2;
                                    }
                                    firstPlayerPadSize += 2;
                                    checkChoice1_2 = true;
                                    BuySuccess();
                                    money -= moneyChoice1_2;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice1_2PositionX && yourChoiceY == choice1_2PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice1_3 == false)
                        {
                            if (yourChoiceX == choice1_3PositionX && yourChoiceY == choice1_3PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice1_3)
                                {
                                    if (firstPlayerPadSize + firstPlayerPosition > 28)
                                    {
                                        firstPlayerPosition -= 3;
                                    }
                                    firstPlayerPadSize += 3;
                                    checkChoice1_3 = true;
                                    BuySuccess();
                                    money -= moneyChoice1_3;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice1_3PositionX && yourChoiceY == choice1_3PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice2_1 == false)
                        {
                            if (yourChoiceX == choice2_1PositionX && yourChoiceY == choice2_1PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice2_1)
                                {
                                    speed += 40;
                                    checkChoice2_1 = true;
                                    BuySuccess();
                                    money -= moneyChoice2_1;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice2_1PositionX && yourChoiceY == choice2_1PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice2_2 == false)
                        {
                            if (yourChoiceX == choice2_2PositionX && yourChoiceY == choice2_2PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice2_2)
                                {
                                    firstPlayerPadSize += 2;
                                    speed += 60;
                                    checkChoice2_2 = true;
                                    BuySuccess();
                                    money -= moneyChoice2_2;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice2_2PositionX && yourChoiceY == choice2_2PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice2_3 == false)
                        {
                            if (yourChoiceX == choice2_3PositionX && yourChoiceY == choice2_3PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice2_3)
                                {
                                    speed += 100;
                                    checkChoice2_3 = true;
                                    BuySuccess();
                                    money -= moneyChoice2_3;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice2_3PositionX && yourChoiceY == choice2_3PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice3_1 == false)
                        {
                            if (yourChoiceX == choice3_1PositionX && yourChoiceY == choice3_1PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice3_1)
                                {
                                    prob -= 5;
                                    checkChoice3_1 = true;
                                    BuySuccess();
                                    money -= moneyChoice3_1;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice3_1PositionX && yourChoiceY == choice3_1PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice3_2 == false)
                        {
                            if (yourChoiceX == choice3_2PositionX && yourChoiceY == choice3_2PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice3_2)
                                {
                                    prob -= 10; ;
                                    checkChoice3_2 = true;
                                    RemoveObject(boxRow / 3 * 2, 20, boxRow / 3 - 1);
                                    RemoveObject(boxRow / 3 * 2, 21, boxRow / 3 - 1);
                                    BuySuccess();
                                    money -= moneyChoice3_2;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }

                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice3_2PositionX && yourChoiceY == choice3_2PositionY)
                            {
                                Bought();
                            }
                        }

                        if (checkChoice3_3 == false)
                        {
                            if (yourChoiceX == choice3_3PositionX && yourChoiceY == choice3_3PositionY)
                            {
                                for (int i = 1; i <= 28; i++)
                                {
                                    RemoveObject(1, i, boxRow / 3 * 2 - 2);
                                }
                                if (money >= moneyChoice3_2)
                                {
                                    prob -= 20;
                                    checkChoice3_3 = true;
                                    RemoveObject(boxRow / 3 * 2, 20, boxRow / 3 - 1);
                                    RemoveObject(boxRow / 3 * 2, 21, boxRow / 3 - 1);
                                    BuySuccess();
                                    money -= moneyChoice3_3;
                                    RemoveObject(boxRow / 3 * 2 + 11, 3, 3);
                                    Console.WriteLine(money);
                                }
                                else
                                {
                                    NotEnough();
                                }
                            }
                        }
                        else
                        {
                            if (yourChoiceX == choice3_3PositionX && yourChoiceY == choice3_3PositionY)
                            {
                                Bought();
                            }
                        }
                    }
                }
            }
        }
        static void Champion()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 3);
            Console.WriteLine("██████████████████");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 4);
            Console.WriteLine("█ ██████████████ █");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 5);
            Console.WriteLine("█ ██████████████ █");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 6);
            Console.WriteLine("█  ████████████  █");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 7);
            Console.WriteLine("█   ██████████   █");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 8);
            Console.WriteLine("█    ████████    █");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 9);
            Console.WriteLine("       ████       ");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 10);
            Console.WriteLine("      ██████      ");
            Console.SetCursorPosition(boxRow * 5 / 6 - 9, 11);
            Console.WriteLine("     ████████     ");
            Console.SetCursorPosition(boxRow * 5 / 6 - 12, 13);
            Console.WriteLine("Bảng xếp hạng người chơi");
            Console.SetCursorPosition(boxRow * 5 / 6 - 12, 13);
            Console.SetCursorPosition(boxRow * 5 / 6 - 3, 14);
            Console.WriteLine("TOP 5:");
            TakeBoard();
            PrintBoard();
            Console.ResetColor();
            
        }
        #endregion
        #endregion // END FUNCTION
        static void Main(string[] args)
        {
            Console.WindowWidth = 130;
            Console.WindowHeight = 40;
            Console.CursorVisible = false;
            Console.OutputEncoding = global::System.Text.Encoding.UTF8;
            Task.Run(() =>
            {
                while (true)
                {
                    PlaySound();
                }
            });
            RemoveScrollBars(); // Xóa bỏ thanh Scroll
            Label:
            DrawMainBox(); // Vẽ khung chính
            StartPage(); // Màn hình bắt đầu sẽ xuất hiện 
            SetInitialPositions(); // Cài đặt vị trí ban đầu của các đối tượng
            SetBall2AtTheMiddleOfTheGameField(); // Cài đặt vị trí ban đầu của quả bóng 2 (Cho level 3)
            Champion();
            PrintPlayerPoint();
            DrawLevel(1);
            while (true) // Dùng để di chuyển các đối tượng
            {
                PrintPlayerPoint();
                Champion();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveFirstPlayerUp();
                        RemovePlayer1(keyInfo); // Xóa đi vết cũ của player 1 khi nó đi lên
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveFirstPlayerDown();
                        RemovePlayer1(keyInfo); // Xóa đi vết cũ của player 1 khi nó đi xuống
                    }
                    if (keyInfo.Key == ConsoleKey.Q) // Nhấn Q => Dừng chương trình
                    {
                        PauseFunction();
                    }
                    if (keyInfo.Key == ConsoleKey.W) // Nhấn W => Dừng chương trình để mở shop
                    {
                        for (int i = 1; i <= 26; i++)
                            RemoveObject(boxRow / 3 * 2, i, boxRow / 3 - 2);
                        MainShop();
                    }
                }
                AIPlayerAIMove();
                if (ball1PositionX == boxRow / 3 * 2 - 2 || ball1PositionX == 1)
                    DrawAIPlayer();
                MoveBall1(); // Dịch chuyện vị trí tiếp theo của quả banh (vết cũ vẫn còn)
                if (heart == 0)
                {
                    Console.SetCursorPosition(boxRow / 3 - 1 - 4, boxColumn / 2);
                    Console.WriteLine("You Lose !");
                    Console.SetCursorPosition(boxRow / 3 * 2 + 4, 15);
                    Console.WriteLine(heart);
                    Console.ReadKey();
                    break;
                }
                RemoveBall1(); // Xóa vết cũ đi
                DrawBall(ball1PositionX, ball1PositionY); // In ra quả banh ở vị trí mới
                if (firstPlayerResult >= 20) // Nếu điểm lớn hơn ... thì sẽ nhảy vào level 3 (Có thêm một quả bóng nữa)
                {
                    Level3();
                    speed += 5;
                }
                DrawFirstPlayer(); // In ra thanh dọc player 1 tại vị trí mới sau khi nhấn một phím để di chuyển
                DrawAIPlayer(); // In ra thanh dọc AI tại vị trí mới sau khi di chuyển
                Thread.Sleep(speed); // Tốc độ quả bóng
            }
            money += 0;
            firstPlayerResult = 0;
            heart = 5;
            goto Label;
        }

        static void PlaySound()
        {
            const int soundLenght = 100;
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1320, soundLenght);
            Console.Beep(1188, soundLenght);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 6);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1056, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Thread.Sleep(soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1408, soundLenght * 2);
            Console.Beep(1760, soundLenght * 4);
            Console.Beep(1584, soundLenght * 2);
            Console.Beep(1408, soundLenght * 2);
            Console.Beep(1320, soundLenght * 6);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 4);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1056, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Thread.Sleep(soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1320, soundLenght);
            Console.Beep(1188, soundLenght);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 6);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1056, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Thread.Sleep(soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1408, soundLenght * 2);
            Console.Beep(1760, soundLenght * 4);
            Console.Beep(1584, soundLenght * 2);
            Console.Beep(1408, soundLenght * 2);
            Console.Beep(1320, soundLenght * 6);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1188, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(990, soundLenght * 4);
            Console.Beep(990, soundLenght * 2);
            Console.Beep(1056, soundLenght * 2);
            Console.Beep(1188, soundLenght * 4);
            Console.Beep(1320, soundLenght * 4);
            Console.Beep(1056, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Console.Beep(880, soundLenght * 4);
            Thread.Sleep(soundLenght * 4);
            Console.Beep(660, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(594, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(440, soundLenght * 8);
            Console.Beep(419, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(660, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(594, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(528, soundLenght * 4);
            Console.Beep(660, soundLenght * 4);
            Console.Beep(880, soundLenght * 8);
            Console.Beep(838, soundLenght * 16);
            Console.Beep(660, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(594, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(440, soundLenght * 8);
            Console.Beep(419, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(660, soundLenght * 8);
            Console.Beep(528, soundLenght * 8);
            Console.Beep(594, soundLenght * 8);
            Console.Beep(495, soundLenght * 8);
            Console.Beep(528, soundLenght * 4);
            Console.Beep(660, soundLenght * 4);
            Console.Beep(880, soundLenght * 8);
            Console.Beep(838, soundLenght * 16);
        }
    }
}
