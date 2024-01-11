using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
 

namespace Hotel.Domain.Managers
{
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public IReadOnlyList<Activity> GetActivities(string filter)
        {
            try
            {
                return _activityRepository.GetActivity(filter);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetActivities", ex);
            }
        }

        public void AddActivity(Activity activity)
        {
            ValidateActivity(activity); // Perform validations before creating the activity

            try
            {
                _activityRepository.AddActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("AddActivity", ex);
            }
        }

        private void ValidateActivity(Activity activity)
        {
            // Validate activity name
            if (string.IsNullOrWhiteSpace(activity.Title) || activity.Title.Length > 500)
            {
                throw new ActivityManagerException("Invalid activity name. It must have a maximum length of 500 characters and must not be empty.");
            }

            // Validate activity date
            if (activity.Date < DateTime.Today)
            {
                throw new ActivityManagerException("Invalid activity date. It must be a future date.");
            }

            // Additional validations can be added based on your requirements
        }
    }
}
