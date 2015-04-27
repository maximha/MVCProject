using MVCProject.ApiModels;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Helpers
{
    public class ResultHelper
    {
        public static ResultModel boolResult(bool result, string message)
        {
            return new ResultModel()
            {
                result = result,
                message = message
            };
        }
        public static LogModel loginResult(string userName, string userKey)
        {
            /*
             * need implement of aes key created and encrypted by rsa key "userKey"
             */
            LogModel logModel = new LogModel
            {
                result = true,
                publicKey = userKey
            };
            return logModel;
        }

        public static workoutModelList workoutsRessult(String _userName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            List<String> names = db.workouts.Where(s => s.userName == _userName).Select(s => s.workoutName).ToList();
            db.Dispose();
            workoutModelList workoutModel = new workoutModelList
            {
                result = true,
                workouts = names
            };
            return workoutModel;
        }

        public static taskModelList tasksRrssult(String _workoutName) 
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            List<String> names = db.tasks.Where(s => s.workoutName == _workoutName).Select(s => s.taskName).ToList();
            db.Dispose();

            taskModelList taskModel = new taskModelList 
            {
                result = true,
                tasksList = names
            };
            return taskModel;
        }

        public static ItemTaskModel taskByNameResult(String _taskName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            task _task = db.tasks.Where(x => x.taskName == _taskName).SingleOrDefault();
            db.Dispose();
            ItemTaskModel taskModel = new ItemTaskModel 
            {
                result = true,
                itemTask = _task
            };
            return taskModel;
        }
        
    }
}