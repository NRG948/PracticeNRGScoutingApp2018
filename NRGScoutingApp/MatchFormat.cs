﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NRGScoutingApp
{
    public class MatchFormat
    {
        //Object to store all params
        public class EntryParams
        {
            public String team { get; set; }
            public int matchNum { get; set; }
            public int side { get; set; }

            public bool crossedB { get; set; }
            public bool allyItem1 { get; set; }
            public bool allyItem2 { get; set; }
            public bool oppItem1 { get; set; }
            public bool oppItem2 { get; set; }

            public bool death { get; set; }
            public bool noClimb { get; set; }
            public bool soloClimb { get; set; }
            public bool giveAssistClimb { get; set; }
            public bool needAssistClimb { get; set; }
            public bool onClimbArea { get; set; }

            public int fouls { get; set; }
            public bool yellowCard { get; set; }
            public bool redCard { get; set; }
            public String comments { get; set; }

        }

        public enum DROP_TYPE {
            dropNone,
            drop1, //Ally Scale
            drop2, //Ally Switch
            drop3,  // Opp. Switch
            dropItemCollector //Vault
        }

        public enum ACTION_TYPE
        {
            pickItem =-2, //Picked Cube
            startClimb,
        }

        public class Data //One MatchEvent (When it happened and what happened
        {
            public int time { get; set; }
            public int type { get; set; }
        }
        public static String eventsListToJSONEvents(List<Data> datas){

            JObject events = new JObject();
            Data[] eventArray = sortListByTime(datas);
            for (int i = 0; i  < eventArray.Length; i++) {
                events.Add("TE" + i + "_0", eventArray[i].time);
                events.Add("TE" + i + "_1", eventArray[i].type);
            }
            return events.ToString(); 
            }

        public static Data[] sortListByTime(List<Data> datas) {
            Data[] input = datas.ToArray();
            for(int i =0; i<input.Length; i++) {
                Console.WriteLine(input[i].time);
                Console.WriteLine(input[i].type);
            }
            Data[] outputArray = new Data[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                outputArray[i] = input[i];
            }
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length - i; j++)
                {
                    // Use ">" for ascending and "<" for descending 
                    if (outputArray[i].time > outputArray[j + i].time)
                    {
                        MatchFormat.Data c = outputArray[i];
                        MatchFormat.Data d = outputArray[j + i];
                        outputArray[i] = d;
                        outputArray[j + i] = c;

                    }
                }
            }
            return outputArray;
        }

        public class EntryEvents {

        }
    }
}
