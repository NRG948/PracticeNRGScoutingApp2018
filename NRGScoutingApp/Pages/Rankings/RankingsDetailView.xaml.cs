﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NRGScoutingApp {
    public partial class RankingsDetailView : ContentPage {
        /*
         * This is the order in which the array is ordered       
         * overall, cargoTime, hatchTime, climb, lvl1, lvl2, lvl3
         */
        public RankingsDetailView (String[] times) {
            InitializeComponent ();
            setScoreValues (times);
            pitButton.IsVisible = Rankings.pitTeams.Contains (Rankings.teamSend);
            String team = Rankings.teamSend.Split ('-', 2) [MatchFormat.teamNameOrNum].Trim ();
            listView.ItemsSource = Matches.matchesList.Where (matchesList => matchesList.teamNameAndSide.ToLower ().Contains (team.ToLower ()));
        }

        void setScoreValues (String[] times) {
            score0.Text = ConstantVars.scoreBaseVals[0] + times[0];
            score1.Text = ConstantVars.scoreBaseVals[1] + times[1];
            score2.Text = ConstantVars.scoreBaseVals[2] + times[2];
            score3.Text = ConstantVars.scoreBaseVals[3] + times[3];
            score4.Text = ConstantVars.scoreBaseVals[4] + times[4];
            score5.Text = ConstantVars.scoreBaseVals[5] + times[5];
            score6.Text = ConstantVars.scoreBaseVals[6] + times[6];
            score7.Text = ConstantVars.scoreBaseVals[7] + times[7];
        }

        async void matchTapped (object sender, Xamarin.Forms.ItemTappedEventArgs e) {
            int jsonIndex = Matches.matchesList.IndexOf (e.Item as Matches.MatchesListFormat);

            await Task.Run (async () => {
                JObject val = JObject.Parse (MatchesDetailView.returnMatchJSONText (jsonIndex));
                JObject parameters = new JObject ();
                foreach (var x in val) {
                    if (!x.Key.Equals ("numEvents")) {
                        parameters.Add (x.Key, x.Value);
                    } else {
                        break;
                    }
                }
                Preferences.Set ("tempParams", JsonConvert.SerializeObject (parameters.ToObject<MatchFormat.EntryParams> ()));
                NewMatchStart.events = MatchFormat.JSONEventsToObject (val);
                CubeDroppedDialog.saveEvents ();
                Preferences.Set ("timerValue", Convert.ToInt32 (val.Property ("timerValue").Value));
                Preferences.Set ("teamStart", val.Property ("team").Value.ToString ());
                Device.BeginInvokeOnMainThread (() => {
                    Navigation.PushAsync (new MatchEntryEditTab () { Title = val.Property ("team").Value.ToString () });
                });
            });
        }

        async void pitClicked (object sender, System.EventArgs e) {
            Preferences.Set ("teamStart", Rankings.teamSend);
            await Navigation.PushAsync (new PitEntry (true, Rankings.teamSend, false) { Title = Rankings.teamSend });
        }
    }
}