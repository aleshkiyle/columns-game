﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Columns.Record
{
    /// <summary>
    /// Утилита для чтения и записи рекордов игры в файл
    /// </summary>
    public class RecordsFileUtility
    {

        /// <summary>
        /// Файл с рекордами (путь/название)
        /// </summary>
        private const string RECORDS_FILE = "records.xml";

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        private static readonly RecordsFileUtility _instance = new RecordsFileUtility();

        /// <summary>
        /// Сериализатор для записи/чтения в/из xml файла
        /// </summary>
        private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(List<Player>));

        /// <summary>
        /// Экземпляр класса (singletone)
        /// </summary>
        public static RecordsFileUtility Instance => _instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private RecordsFileUtility() { }

        /// <summary>
        /// Прочитать рекорды из файла
        /// </summary>
        /// <returns></returns>
        public List<Player> ReadRecordsFromFile()
        {
            List<Player> players = new List<Player>();
            try
            {
                using (FileStream fs = new FileStream(RECORDS_FILE, FileMode.OpenOrCreate))
                {
                    players = _xmlSerializer.Deserialize(fs) as List<Player>;
                }
            }
            catch
            {
                return new List<Player>();
            }
            return players;
        }

        /// <summary>
        /// Запись рекорда в файл
        /// </summary>
        /// <param name="parPlayer">Игрок</param>
        public void WriteRecordToFile(Player parPlayer)
        {
            List<Player> players = ReadRecordsFromFile();
            players.Add(parPlayer);
            WriteRecordsToFile(players);
        }

        /// <summary>
        /// Запись рекордов в файл
        /// </summary>
        /// <param name="parPlayers">Игроки</param>
        public void WriteRecordsToFile(List<Player> parPlayers)
        {
            using (FileStream fs = new FileStream(RECORDS_FILE, FileMode.OpenOrCreate))
            {
                _xmlSerializer.Serialize(fs, parPlayers);
            }
        }

    }
}
