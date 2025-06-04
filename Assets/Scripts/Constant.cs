using UnityEngine;

public static class Constant
{
    //public const string URL = "http://localhost:5110";
    public const string URL = "http://dksysd-home-server.duckdns.org:8080/";

    public static string WEBSOCKET_URL(string websocketToken, string steamId)
    {
        //URL 인코딩하기
        return "ws://dksysd-home-server.duckdns.org:8080/api/matchmaking?websocket_token=" + websocketToken +
               "&steam_id=" + steamId;
        //return "ws://localhost:5110/api/matchmaking?websocket_token=" + websocketToken + "&steam_id=" + steamId;
    }

    public const string SERVER_IP = "192.168.219.104";
    public const int SERVER_PORT = 9009;
    public const int ISHOST = 0;
    public const int IP_ADDRESS = 1;
    public const int PORT = 2;
    public const KeyCode EMPTY = KeyCode.Q;
    public const int REFRESHINTERVAL = 270000;

    public const int TYPE = 0;

    public static class RestAPI
    {
        public static class Auth
        {
            public const string REGIST = "/api/auth/register";
            public const string LOGIN = "/api/auth/login";
            public const string LOGOUT = "/api/auth/logout";
            public const string REFRESH = "/api/auth/refresh";
            public const string PLAYERLOGIN = "/api/auth/loginPlayer";
            public const string WEBSOCKET_TOKEN = "/api/auth/websocket-token";
        }

        public static class Player
        {
            public const string PLAYERLIST = "/api/player/list";
        }

        public static class Character
        {
            public const string ALL = "/api/character/all";
        }

        public static class CustomCommand
        {
            public const string ALL = "/api/customCommand/all";
            public const string SET = "/api/customCommand/set";
        }
    }

    public static class Scene
    {
        public const string GAME_SCENE = "Scenes/GameScene1";
        public const string HOME_SCENE = "Scenes/HomeScene";
        public const string USER_MAIN_SCENE = "Scenes/UserMainScene";
        public const string ADMINISTRATOR_SCENE = "Scenes/Administer";
    }

    public static class SteamNetworkingType
    {
        public const char DELIMITER = '>';
        public const int PINGTEST = 0;

        public const int CONNECTION = 1;
        public const int KEYINPUT = 2;

        public static class KeyInput
        {
            public const int MOVEMENT = 0;

            //public const int JUMP = 1;
            public const int SKILL = 2;
        }
    }

    public static class SkillName
    {
        public const int PUNCH = 0;
        public const int JUMP_PUNCH = 1;

        public static class Naktis
        {
            public const int HASEGI = 2;
            public const int SCRATCH = 3;
            public const int UPPERWING = 4;
            public const int FLY = 5;
        }

        public static class Kagetsu
        {
        }
    }

    /*
        Punch = Atk_Punch
     */
}