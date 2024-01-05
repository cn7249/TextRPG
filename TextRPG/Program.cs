// 프로그래밍 기초 개인 과제 - 텍스트 RPG 게임 만들기

using System;
using System.Diagnostics.Contracts;

public class Player
{
    // 플레이어의 능력치
    public int level = 1;
    public string name = "Chad";
    public string job = "전사";
    public int atk = 10;
    public int def = 5;
    public int hp = 100;
    public int gold = 1500;

    public int initAtk = 10;
    public int initDef = 5;

    // 플레이어의 인벤토리
    public bool[] inventory = new bool[10];


    // 플레이어의 최초 생성 시 설정값
    public void PlayerStart()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = false;
        }
    }

    // 플레이어의 행동
    public int InputAction()
    {
        int num = 0;
        while (true)
        {
            Console.Write("원하시는 행동을 입력해주세요.\n>>");
            string input = Console.ReadLine();
            bool isInt;
            isInt = int.TryParse(input, out num);

            if (isInt)
            {
                break;
            }
            else
            {
                Console.WriteLine("잘못된 값입니다. 화면에 표시된 정수를 입력해주세요.");
            }
        }
        return num;
    }

    public void AddInventory(int select)
    {
        inventory[select] = true;
    }

    public void PayGold(int amount)
    {
        gold -= amount;
    }

    public void OnEquipment(int amount, bool isWeapon)
    {
        if (isWeapon)
        {
            atk += amount;
        }
        else
        {
            def += amount;
        }
    }

    public void OffEquipment(int amount, bool isWeapon)
    {
        if (isWeapon)
        {
            atk -= amount;
        }
        else
        {
            def -= amount;
        }
    }
}

public class Item
{
    public string name = "";
    public int point = 0;
    public string comment = "";
    public int reqGold = 0;

    public bool isWeapon = false; // false면 보호구(Armor), true면 무기류(Weapon)
    public bool isBought = false;
    public bool isEquiped = false;
}

class GameScene
{
    Player player = new Player();
    Item[] itemsArr = new Item[10];

    bool isInvenDisplay = true; // true면 Display, false면 Equip Mode
    bool isShopDisplay = true; // true면 Display, false면 Buy Mode

    void MakeItems()
    {
        // 아이템 배열 생성
        for (int i = 0; i < itemsArr.Length; i++)
        {
            itemsArr[i] = new Item();
        }

        // 아이템 정보 입력
        itemsArr[0].name = "수련자 갑옷";
        itemsArr[0].comment = "수련에 도움을 주는 갑옷입니다.";
        itemsArr[0].point = 5;
        itemsArr[0].reqGold = 1000;

        itemsArr[1].name = "무쇠갑옷";
        itemsArr[1].comment = "무쇠로 만들어져 튼튼한 갑옷입니다.";
        itemsArr[1].point = 9;
        itemsArr[1].reqGold = 2000;

        itemsArr[2].name = "스파르타의 갑옷";
        itemsArr[2].comment = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
        itemsArr[2].point = 15;
        itemsArr[2].reqGold = 3500;

        itemsArr[3].name = "낡은 검";
        itemsArr[3].comment = "쉽게 볼 수 있는 낡은 검 입니다.";
        itemsArr[3].point = 2;
        itemsArr[3].reqGold = 600;
        itemsArr[3].isWeapon = true;

        itemsArr[4].name = "청동 도끼";
        itemsArr[4].comment = "어디선가 사용됐던 것 같은 도끼입니다.";
        itemsArr[4].point = 5;
        itemsArr[4].reqGold = 1500;
        itemsArr[4].isWeapon = true;

        itemsArr[5].name = "스파르타의 창";
        itemsArr[5].comment = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
        itemsArr[5].point = 7;
        itemsArr[5].reqGold = 3000;
        itemsArr[5].isWeapon = true;
    }

    public void GameInit()
    {
        MakeItems(); // 아이템 생성
        player.PlayerStart(); // 플레이어 초기값 생성
    }



    // 게임 Scene 관련 메서드
    public void ViewMain()
    {
        Console.Clear(); // 콘솔 창 깔끔하게 하기

        Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n\n");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점\n");

        int choice = player.InputAction();
        switch (choice)
        {
            case 1: ViewStatus(); break;
            case 2: ViewInventory(); break;
            case 3: ViewShop(); break;
            default: 
                Console.WriteLine("잘못된 입력입니다."); 
                Console.ReadLine(); // 사용자 입력까지 대기
                ViewMain();
                break;
        }
    }

    void ViewStatus()
    {
        Console.Clear();

        int equipAtk = player.atk - player.initAtk;
        int equipDef = player.def - player.initDef;
        string addAtk = "";
        string addDef = "";

        if (equipAtk != 0)
        {
            addAtk = " (+" + equipAtk + ")";
        }

        if (equipDef != 0)
        {
            addDef = " (+" + equipDef + ")";
        }

        Console.Write("상태 보기\n캐릭터의 정보가 표시됩니다.\n\n");
        Console.WriteLine("Lv. " + player.level);
        Console.WriteLine("{0} ( {1} )", player.name, player.job);
        Console.WriteLine("공격력 : " + player.atk + addAtk);
        Console.WriteLine("방어력 : " + player.def + addDef);
        Console.WriteLine("체 력 : " + player.hp);
        Console.WriteLine("Gold : {0} G\n", player.gold);
        Console.WriteLine("0. 나가기\n");

        int choice = player.InputAction();
        if (choice == 0)
        {
            ViewMain();
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadLine(); // 사용자 입력까지 대기
            ViewStatus();
        }
    }

    void ViewInventory()
    {
        Console.Clear();
        Console.Write("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n");
        Console.WriteLine("[아이템 목록]");

        if (isInvenDisplay)
        {
            InvenDisplayMode();
        }
        else
        {
            InvenEquipMode();
        }
    }
    void InvenDisplayMode()
    {
        for (int i = 0; i < player.inventory.Length; i++)
        {
            if (player.inventory[i]) 
            {
                Console.WriteLine("- {0}{1}    | {2} +{3}  | {4}",
                ((itemsArr[i].isEquiped) ? "[E]" : ""),
                itemsArr[i].name,
                ((itemsArr[i].isWeapon) ? "공격력" : "방어력"),
                itemsArr[i].point,
                itemsArr[i].comment);
            }
        }

        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("0. 나가기\n");

        int choice = player.InputAction();
        if (choice == 0)
        {
            ViewMain();
        }
        else if (choice == 1)
        {
            isInvenDisplay = false;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadLine();
        }
        ViewInventory();
    }
    void InvenEquipMode()
    {
        for (int i = 0; i < player.inventory.Length; i++)
        {
            if (player.inventory[i])
            {
                Console.WriteLine("- {0} {1}{2}    | {3} +{4}  | {5}",
                (i+1),
                ((itemsArr[i].isEquiped) ? "[E]" : ""),
                itemsArr[i].name,
                ((itemsArr[i].isWeapon) ? "공격력" : "방어력"),
                itemsArr[i].point,
                itemsArr[i].comment);
            }
        }

        Console.WriteLine("\n0. 나가기\n");

        int choice = player.InputAction();
        if (choice == 0)
        {
            isInvenDisplay = true;
            ViewInventory();
        }
        else
        {
            int select = choice - 1;

            if (itemsArr[select].isBought == true)
            {
                if (itemsArr[select].isEquiped)
                {
                    itemsArr[select].isEquiped = false;
                    player.OffEquipment(itemsArr[select].point, itemsArr[select].isWeapon);
                    Console.WriteLine("{0}을(를) 장착 해제했습니다.", itemsArr[select].name);
                }
                else
                {
                    itemsArr[select].isEquiped = true;
                    player.OnEquipment(itemsArr[select].point, itemsArr[select].isWeapon);
                    Console.WriteLine("{0}을(를) 장착 완료했습니다.", itemsArr[select].name);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadLine();
            ViewInventory();
        }
    }

    void ViewShop()
    {
        Console.Clear();

        Console.Write("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n");
        Console.WriteLine("[보유 골드]\n{0} G\n", player.gold);
        Console.WriteLine("[아이템 목록]");

        if (isShopDisplay)
        {
            ShopDisplayMode();
        }
        else
        {
            ShopBuyMode();
        }

    }
    void ShopDisplayMode()
    {
        for (int i = 0; i < itemsArr.Length; i++)
        {
            if (itemsArr[i].name != "") // 아이템 이름이 공백이 아닌 경우만
            {
                Console.WriteLine("- {0}    | {1} +{2}  | {3}    | {4}",
                itemsArr[i].name,
                ((itemsArr[i].isWeapon) ? "공격력" : "방어력"), // isWeapon이 참이면 공격력, 거짓이면 방어력 반환
                itemsArr[i].point,
                itemsArr[i].comment,
                ((itemsArr[i].isBought == false) ? (itemsArr[i].reqGold + " G") : "구매완료")); // isBought가 거짓이면 가격, 참이면 구매완료 표시
            }
        }

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("0. 나가기\n");

        int choice = player.InputAction();
        if (choice == 0)
        {
            ViewMain();
        }
        else if (choice == 1)
        {
            isShopDisplay = false;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadLine(); // 사용자 입력까지 대기
        }
        ViewShop();
    }
    void ShopBuyMode()
    {
        for (int i = 0; i < itemsArr.Length; i++)
        {
            if (itemsArr[i].name != "") 
            {
                Console.WriteLine("- {0} {1}    | {2} +{3}  | {4}    | {5}",
                (i+1),
                itemsArr[i].name,
                ((itemsArr[i].isWeapon) ? "공격력" : "방어력"), 
                itemsArr[i].point,
                itemsArr[i].comment,
                ((itemsArr[i].isBought == false) ? (itemsArr[i].reqGold + " G") : "구매완료"));
            }
        }

        Console.WriteLine("\n0. 나가기\n");

        int choice = player.InputAction();
        if (choice == 0)
        {
            isShopDisplay = true;
            ViewShop();
        }
        else
        {
            int select = choice - 1;

            if (itemsArr[select].isBought == false)
            {
                // 보유 골드 체크 기능 추가할 것
                if (player.gold >= itemsArr[select].reqGold)
                {
                    player.AddInventory(select);
                    player.PayGold(itemsArr[select].reqGold);
                    itemsArr[select].isBought = true;
                    Console.WriteLine("구매를 완료했습니다!");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다!");
                }
            }
            else
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }

            Console.ReadLine();
            ViewShop();
        }
    }

}



class Program
{
    static void Main(string[] args)
    {
        GameScene MainScene = new GameScene();
        MainScene.GameInit(); // 게임 초기값 로드
        MainScene.ViewMain(); // 메인 화면 로드
    }
}