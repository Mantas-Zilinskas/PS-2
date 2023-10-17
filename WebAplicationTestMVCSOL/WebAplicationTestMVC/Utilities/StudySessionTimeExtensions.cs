﻿using System;

namespace WebAplicationTestMVC.Utilities
{
    public static class StudySessionTimeExtensions
    {
        public static string FormatDuration(this StudySessionTime session)
        {
            int hours = session.Duration.Hours;
            int minutes = session.Duration.Minutes;
            int seconds = session.Duration.Seconds;

            return $"{hours} hours {minutes} minutes {seconds} seconds";
        }
    }
}