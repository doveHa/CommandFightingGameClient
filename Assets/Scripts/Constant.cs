using UnityEngine;

public static class Constant
{
    //public const string URL = "http://localhost:5110";
    public const string URL = "http://dksysd-home-server.duckdns.org:8080/";

    public static string WEBSOCKET_URL(string websocketToken, string steamId)
    {
        //URL 인코딩하기
        return "ws://dksysd-home-server.duckdns.org:8080/api/matchmaking?websocket_token=" + websocketToken + "&steam_id=" + steamId;
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
        public const string GAME_SCENE = "Scenes/GameScene";
        public const string HOME_SCENE = "Scenes/HomeScene";
        public const string USER_MAIN_SCENE = "Scenes/UserMainScene";
        public const string ADMINISTRATOR_SCENE = "Scenes/Administer";
    }

    public static class SteamNetworkingType
    {
        //Hadove02 계정 SteamID
        //public const string REMOTESTEAMID = "76561198853166461";

        //doveHa02 계정 SteamID
        //public const string REMOTESTEAMID = "76561199834491206";
        public const int CONNECTION = 0;

        //Data는 frame + " " + input의 형태로 전송
        public const int MOVEMENT = 1;
    }

    public const int SELECTED_CHARACTER = 0;
}