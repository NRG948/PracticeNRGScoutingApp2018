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
        public enum ACTION
        {
            dropNone, //Drop None
            drop1, //Rocket lvl 1
            drop2, //Rocket lvl 2
            drop3, //Rocket lvl 3
            drop4, //Cargo Ship
            pick1, //Picked Hatch Panel
            pick2, //Picked Cargo
            startClimb //Start Climb
        }

        public enum MATCH_SIDES
        {
            Red1,
            Red2,
            Red3,
            Blue1,
            Blue2,
            Blue3
        }

        public class Data //One MatchEvent (When it happened and what happened
        {
            public int time { get; set; }
            public int type { get; set; }
        }
        public static JObject eventsListToJSONEvents(List<Data> datas)
        {

            JObject events = new JObject();
            Data[] eventArray = sortListByTime(datas);
            events.Add("numEvents", eventArray.Length);
            for (int i = 0; i < eventArray.Length; i++)
            {
                events.Add("TE" + i + "_0", eventArray[i].time);
                events.Add("TE" + i + "_1", eventArray[i].type);
            }
            return events;
        }

        public static Data[] sortListByTime(List<Data> datas)
        {
            Data[] input = datas.ToArray();
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

        public static String matchSideFromEnum(int side)
        {
            switch (side)
            {
                case (int)MATCH_SIDES.Red1:
                    return ConstantVars.RED_1_TEXT;
                case (int)MATCH_SIDES.Red2:
                    return ConstantVars.RED_2_TEXT;
                case (int)MATCH_SIDES.Red3:
                    return ConstantVars.RED_3_TEXT;
                case (int)MATCH_SIDES.Blue1:
                    return ConstantVars.BLUE_1_TEXT;
                case (int)MATCH_SIDES.Blue2:
                    return ConstantVars.BLUE_2_TEXT;
                case (int)MATCH_SIDES.Blue3:
                    return ConstantVars.BLUE_3_TEXT;
            }
            return "Error";
        }

        public static int teamNameOrNum = 1;
    }
}