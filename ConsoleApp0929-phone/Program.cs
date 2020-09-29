using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp0929_phone
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBookManager manager = PhoneBookManager.CreatebookManager();

            while(true)
            {
                manager.ShowMenu();
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice < 0 || choice > 6)
                        throw new Exception("1~6사이의 숫자를 입력주세요");

                    switch (choice)
                    {
                        case 1: manager.InputData(); break;
                        case 2: manager.ListData(); break;
                        case 3: manager.SortData(); break;
                        case 4: manager.SearchData(); break;
                        case 5: manager.DeleteData(); break;
                        case 6: Console.WriteLine("프로그램을 종료합니다"); return;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }
    }
}
