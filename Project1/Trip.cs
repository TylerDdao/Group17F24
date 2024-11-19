﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Group17-Rob-Maksym-Ginbot-Dao
namespace Project1
{
    internal class Trip
    {
        //Variables.
        int score;
        string startingLocation;
        string endLocation;
        TimeSpan duration;
        //Constructor. Non-para.
        public Trip()
        {
            //Assign values.
            score = 0;
            startingLocation = "Default Starting Location";
            endLocation = "Default End Location";
            //Duration of 0 hours, 0 minutes, 0 seconds
            duration = new TimeSpan(0, 0, 0); 
        }

        //Constructor. Para.
        public Trip(int score, string startingLocation, string endLocation, TimeSpan duration)
        {
            this.score = score;
            this.startingLocation = startingLocation;
            this.endLocation = endLocation;
            this.duration = duration;
        }

        //Getter and Setter for the scores.
        public int GetScore()
        {
            return score;
        }
        public void SetScore(int score)
        {
            this.score = score;
        }

        //Getter and Setter for startingLocation.
        public string GetStartingLocation()
        {
            return startingLocation;
        }
        public void SetStartingLocation(string startingLocation)
        {
            this.startingLocation = startingLocation;
        }

        //Getter and Setter for endLocation.
        public string GetEndLocation()
        {
            return endLocation;
        }
        public void SetEndLocation(string endLocation)
        {
            this.endLocation = endLocation;
        }
        // Getter and Setter for duration.
        public TimeSpan GetDuration()
        {
            return duration;
        }
        //Setter for duration.
        public void SetDuration(TimeSpan duration)
        {
            this.duration = duration;
        }
    }
}
