// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

namespace IcarusCommons.Structures.Chat
{
    public enum ChatChannelsEnum
    {
        SearchGroups = 9, //Поиск группы
        Bullhorn = 8, //8 Рупор
        Shout = 7, //7 - Крик в локации
        SystemAdvert = 6, //6 - Сис сообщение вверху экрана желтым цветом
        TradeChannel = 5,
        WhisperMessage = 4,
        GuildChannel = 3,
        GroupChannel = 2,
        WhiteMessage = 1,
        YelowMessage = 0
//5 - трейд канал
//4 - приватное сообщение
//3 - канал гильдии
//2 - группа
//1 - сообщение белым цветом в чате
//0 - сообщение желтым цветом в чате
    }
}