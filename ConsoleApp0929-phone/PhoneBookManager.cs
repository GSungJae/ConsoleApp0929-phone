using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp0929_phone
{

    public class PhoneBookManager
    {
        
        const int MAX_CNT = 100;
        PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];

        int curCnt = 0;

        static PhoneBookManager bookManager;
        public static PhoneBookManager CreatebookManager()
        {
            if (bookManager == null)
                bookManager = new PhoneBookManager();

            return bookManager;
        }
        public void ShowMenu()
        {
            Console.WriteLine("------------------------ 주소록 --------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  |  3. 정렬  |  4. 검색  |  5. 삭제  |  6. 종료");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("선택: ");
            
        }

        public void InputData()
        {
            Console.WriteLine("1.일반  2.대학  3.회사");
            Console.Write("선택 >> ");
            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                    break;
                else
                    throw new Exception("1.일반  2.대학  3.회사 중에 선택하십시오.");
            }
            if (choice < 1 || choice > 3)
            {
                throw new Exception("1.일반  2.대학  3.회사 중에 선택하십시오.");
            }
            PhoneInfo info = null;

            switch (choice)
            {
                case 1:
                    info = InputFriendInfo();
                    break;
                case 2:
                    info = InputUnivInfo();
                    break;
                case 3:
                    info = InputCompanyInfo();
                    break;
            }
            if (info != null)
            {
                infoStorage[curCnt++] = info;
                Console.WriteLine("데이터 입력이 완료되었습니다");
            }

        }

        public void ListData()
        {
            if (curCnt == 0)
            {
                throw new Exception("입력된 데이터가 없습니다.");
            }
            for (int i = 0; i < curCnt; i++)
            {
                infoStorage[i].ShowPhoneInfo();
                // PhoneInfo curInfo = infoStorage[i];
                // curInfo.ShowPhoneInfo();
            }
            Console.WriteLine();
        }
        private string[] InputCommonInfo()
        {
            Console.Write("이름을 입력해주세요(필수). :");

            string name = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(name)) // if (name == "");   if(name.Length <1)    if(name.Equals(""))
            {
                throw new Exception("이름을 입력해주세요.");
            }
            else
            {
                int dataIdx = SearchName(name);
                if (dataIdx > -1)
                {
                    throw new Exception("이미 등록된 이름입니다. 다른 이름으로 입력해주세요.");
                }
            }
            Console.Write("전화번호를 입력해주세요(필수). :");
            string phone = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(phone)) // if (name == "");   if(name.Length <1)    if(name.Equals(""))
            {
                throw new Exception("전화번호를 입력해주세요.");
            }
            Console.Write("생일을 입력해주세요(선택). :");
            string birth = Console.ReadLine().Trim();

            string[] arr = new string[3];

            arr[0] = name;
            arr[1] = phone;
            arr[2] = birth;

            return arr;
        }
        private PhoneInfo InputFriendInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                return null;

            return new PhoneInfo(comInfo[0], comInfo[1], comInfo[2]);
        }

        private PhoneInfo InputUnivInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                return null;

            Console.Write("전공을 입력해주세요(필수): ");
            string major = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(major))
            {
                throw new Exception("전공을 입력해주세요.");
            }

            Console.Write("학년을 입력해주세요(필수): ");
            string year = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(year))
            {
                throw new Exception("학년을 입력해주세요.");
            }

            return new PhoneUnivInfo(comInfo[0], comInfo[1], comInfo[2], major, year);
        }

        private PhoneInfo InputCompanyInfo()
        {
            string[] comInfo = InputCommonInfo();
            if (comInfo == null || comInfo.Length != 3)
                return null;

            Console.Write("회사명을 입력해주세요(필수): ");
            string company = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(company))
            {
                throw new Exception("학년을 입력해주세요.");
            }

            return new PhoneCompanyInfo(comInfo[0], comInfo[1], comInfo[2], company);
        }
        public void SearchData()
        {
            if (curCnt == 0)
                throw new Exception("검색할 값이 없습니다.");

            Console.WriteLine("주소록 검색을 시작합니다.");
            int dataIdx = SearchName();
            if (dataIdx < 0)
                throw new Exception("검색된 데이터가 없습니다.");
            else
                infoStorage[dataIdx].ShowPhoneInfo();
            Console.WriteLine();
        }

        private int SearchName()
        {
            Console.Write("이름을 입력해주세요.: ");
            string name = Console.ReadLine().Trim().ToLower().Replace(" ", "");

            for (int i = 0; i < curCnt; i++)
            {
                // == , Equals(), CompareTo() 문자열 비교할때 주로 사용함
                if (infoStorage[i].Name.CompareTo(name) == 0)// 비교할땐 둘 다 조건을 같게한다.
                {
                    return i;
                }
            }
            return -1;
        }

        private int SearchName(string name)
        {

            name = name.Trim().ToLower().Replace(" ", "");

            for (int i = 0; i < curCnt; i++)
            {
                // == , Equals(), CompareTo() 문자열 비교할때 주로 사용함
                if (infoStorage[i].Name.CompareTo(name) == 0)// 비교할땐 둘 다 조건을 같게한다.
                {
                    return i;
                }
            }
            return -1;
        }
        public void SortData()
        {
            PhoneInfo[] new_arr = new PhoneInfo[curCnt];
            Array.Copy(infoStorage, new_arr, curCnt);

            if (curCnt == 0)
                throw new Exception("정렬할 값이 없습니다.");

            Console.WriteLine("1.이름 오름차순  2.이름 내림차순  3.전화번호 오름차순  4.전화번호 내림차순");
            Console.Write("선택 >> ");

            int choice;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice))
                    break;
                else
                    throw new Exception("1.이름 오름차순  2.이름 내림차순  3.전화번호 오름차순  4.전화번호 내림차순를 입력해주세요"); 
            }
            if (choice < 1 || choice > 4)
            {
                throw new Exception("1.이름 오름차순  2.이름 내림차순  3.전화번호 오름차순  4.전화번호 내림차순 중에 선택하십시오.");
            }
            if (choice == 1)
            {
                Array.Sort(new_arr);
            }
            else if (choice == 2)
            {
                Array.Sort(new_arr);
                Array.Reverse(new_arr);
            }
            else if (choice == 3)
            {
                Array.Sort(new_arr, new PhoneComparator());
            }
            else if (choice == 4)
            {
                Array.Sort(new_arr, new PhoneComparator());
                Array.Reverse(new_arr);
            }
            for (int i = 0; i < curCnt; i++)
            {
                new_arr[i].ShowPhoneInfo();
            }
            Console.WriteLine();

        }
        public void DeleteData()
        {
            if (curCnt == 0)
                throw new Exception("삭제할 값이 없습니다.");

            Console.WriteLine("주소록 삭제를 시작합니다.");

            int dataIdx = SearchName(); 
            if (dataIdx < 0)
                throw new Exception("삭제할 데이터가 없습니다.");
            else
            {
                for (int i=dataIdx; i<curCnt; i++)
                {
                    infoStorage[i] = infoStorage[i + 1];
                }
                curCnt--;
                Console.WriteLine("삭제가 완료되었습니다.");
            }
        }
        public void SaveData()
        {

        }
    }
}

