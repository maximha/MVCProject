using MVCProject.ApiModels;
using MVCProject.Helpers;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class AndroidController : ApiController
    {
        #region API
        //login function
        [System.Web.Http.HttpPost]
        public AbstractModel Login(LoginModel loginModel)
        {
            //check if user allow to logIn
            user user = checkLogin(loginModel.userName, loginModel.password);
            if (user == null)
            {
                return ResultHelper.boolResult(false, "User not found");
            }
            return ResultHelper.arrayResult(user.userName, loginModel.publicKey);
        }

        [System.Web.Http.HttpPost]
        public AbstractModel Registration(RegistrationModel regModel)
        {
            //try to add new user
            bool didAddUser = addUser(regModel.firstName, regModel.lastName, regModel.userName, regModel.password);
            //check if new user added
            if (!didAddUser)
            {
                return ResultHelper.boolResult(false, "Registration failure");
            }
            
            return ResultHelper.boolResult(true, "Registration success");
        }

        //AddWorkout function
        [System.Web.Http.HttpPost]
        public AbstractModel AddWorkout(WorkoutModel workoutModel)
        {
            //check if workoutName allow to workout
            workout _workoutName = checkWorkout(workoutModel.workoutName);
            if (_workoutName == null)
            {
                //try to add new workout
                bool didAddWorkout = addWorkout(workoutModel.workoutName);
                if (!didAddWorkout)
                {
                    return ResultHelper.boolResult(true, "Adding Workout success");
                }
                return ResultHelper.boolResult(false, "Adding Workout failure");        
            }
            return ResultHelper.boolResult(false, "Workout already exist");
        }

        //AddTask function
        [System.Web.Http.HttpPost]
        public AbstractModel AddTask(TaskModel taskModel)
        {
            //check if taskName allow to task
            task _task = checkTask(taskModel.taskName);
            if (_task == null)
            {
                //try to add new task
                bool didAddTask = addTask(taskModel.taskName, taskModel.descriptionTask, taskModel.timeTask, taskModel.revTask);
                if (!didAddTask)
                {
                    return ResultHelper.boolResult(true, "Adding Task success");
                }
                return ResultHelper.boolResult(false, "Adding Task failure");
            }
            return ResultHelper.boolResult(false, "TaskName already exist");
        }
        #endregion

        #region
        private static user checkLogin(string userName, string password)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get user by userName
            user _user = db.users.Where(x => x.userName == userName).SingleOrDefault();

            //check if user exist and if password is correct
            //if (_user == null || !Equals(user.password, CryptHelper.getHash(password)))
            if (_user == null || !user.Equals(password,password))
            {
                return null;
            }
            return _user;
        }

        private static workout checkWorkout(string workoutName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get workout by workoutName
            workout _workout = db.workouts.Where(x => x.workoutName == workoutName).SingleOrDefault();

            //check if workout exist and if workoutName is correct
            //if (_user == null || !Equals(user.password, CryptHelper.getHash(password)))
            if (_workout == null || !workout.Equals(workoutName, workoutName))
            {
                return null;
            }
            return _workout;
        }

        private static task checkTask(string taskName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get workout by workoutName
            task _task = db.tasks.Where(x => x.taskName == taskName).SingleOrDefault();

            //check if workout exist and if workoutName is correct
            //if (_user == null || !Equals(user.password, CryptHelper.getHash(password)))
            if (_task == null || !workout.Equals(taskName, taskName))
            {
                return null;
            }
            return _task;
        }

        //query function to check if user exist in DB
        private static user findUser(string userName)
        {
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get user by userName
            user user = db.users.Where(x => x.userName == userName).SingleOrDefault();
            //check if user exist
            if (user == null)
            {
                return null;
            }
            return user;
        }

        //query function to check if workout exist in DB
        private static workout findWorkout(string workoutName)
        {
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get workout by workoutName
            workout _workout = db.workouts.Where(x => x.workoutName == workoutName).SingleOrDefault();
            //check if workout exist
            if (_workout == null)
            {
                return null;
            }
            return _workout;
        }

        //query function to check if task exist in DB
        private static task findTask(string taskName)
        {
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get task by taskName
            task _task = db.tasks.Where(x => x.taskName == taskName).SingleOrDefault();
            //check if task exist
            if (_task == null)
            {
                return null;
            }
            return _task;
        }

        //query function to add new user to DB
        private static bool addUser(string firstName, string lastName, string userName, string password)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if user exist in DB
            user user = findUser(userName);
            if (user != null)
            {
                return false;
            }
            user = new user();
            //if user not exist create new user
            user.firstName = firstName;
            user.lastName = lastName;
            user.password = password; // CryptHelper.getHash(password);
            user.userName = userName;
            db.users.Add(user);
            /*List<mrs_keys> keys = DB.mrs_keys.ToList<mrs_keys>();
            foreach (mrs_keys key in keys)
            {
                key.Status = false;
                DB.Entry(key).State = EntityState.Modified;
            }*/
            db.SaveChanges();
            return true;
        }

        private static bool addWorkout(string workoutName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if workout exist in DB
            workout newWorkout = findWorkout(workoutName);
            if (newWorkout != null)
            {
                return false;
            }
            newWorkout = new workout();
            //if workout not exist create new workout
            newWorkout.workoutName = workoutName;
            db.workouts.Add(newWorkout);
            /*List<mrs_keys> keys = DB.mrs_keys.ToList<mrs_keys>();
            foreach (mrs_keys key in keys)
            {
                key.Status = false;
                DB.Entry(key).State = EntityState.Modified;
            }*/
            db.SaveChanges();
            return true;
        }

        private static bool addTask(string taskName, string descriptionTask, string timeTask, string revTask)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if task exist in DB
            task newTask = findTask(taskName);
            if (newTask != null)
            {
                return false;
            }
            //---- how to add workoutName??? *** in DataBase exist workoutName
            newTask = new task();
            //if task not exist create new task
            newTask.taskName = taskName;
            newTask.description = descriptionTask;
            newTask.time = timeTask;
            newTask.rev = revTask;
            db.tasks.Add(newTask);
            /*List<mrs_keys> keys = DB.mrs_keys.ToList<mrs_keys>();
            foreach (mrs_keys key in keys)
            {
                key.Status = false;
                DB.Entry(key).State = EntityState.Modified;
            }*/
            db.SaveChanges();
            return true;
        }
        #endregion
    }
}
