using MVCProject.ApiModels;
using MVCProject.Helpers;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        public AbstractModel Login(LoginModel loginModel )
        {
            //LoginModel loginModel = new LoginModel();
            //check if user allow to logIn
            user user = checkLogin(loginModel.userName, loginModel.password);
            if (user == null)
            {
                return ResultHelper.boolResult(false, "User not found");
            }
            return ResultHelper.loginResult(loginModel.userName, loginModel.password);
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

        [System.Web.Http.HttpPost]
        public AbstractModel ChangeProfile(RegistrationModel changeProfModel)
        {
            //check if user exist in DB
            user _user = findUser(changeProfModel.userName);
            if (_user == null)
            {
                return ResultHelper.boolResult(false, "User not found");
            }
            //try to change profile
            bool didChangeProf = changeProfile(changeProfModel.firstName, changeProfModel.lastName, changeProfModel.userName, changeProfModel.password);
            if (!didChangeProf)
            {
                return ResultHelper.boolResult(false, "Change Profile failure");
            }
            
            return ResultHelper.boolResult(true, "Change Profile success");
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
                bool didAddWorkout = addWorkout(workoutModel.userName , workoutModel.workoutName);
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
                bool didAddTask = addTask(taskModel.workoutName, taskModel.taskName, taskModel.descriptionTask, taskModel.timeTask, taskModel.revTask);
                if (didAddTask)
                {
                    return ResultHelper.boolResult(true, "Adding Task success");
                }
                return ResultHelper.boolResult(false, "Adding Task failure");
            }
            return ResultHelper.boolResult(false, "TaskName already exist");
        }

        //Delete function
        [System.Web.Http.HttpPost]
        public AbstractModel DeleteWorkout(String userName, String workoutName)
        {
            //try to delete workout
            bool didDeleteWorkout = deleteWorkout(userName, workoutName);
            if (!didDeleteWorkout)
            {
                return ResultHelper.boolResult(true, "Deleting Workout success");
            }
            return ResultHelper.boolResult(false, "Deleting Workout failure");   
        }
        [System.Web.Http.HttpPost]
        public AbstractModel DeleteTask(String workoutName, String taskName)
        {
            //try to delete task
            bool didDeleteTask = deleteTask(workoutName, taskName);
            if (!didDeleteTask)
            {
                return ResultHelper.boolResult(true, "Deleting Task success");
            }
            return ResultHelper.boolResult(false, "Deleting Task failure");
        }
        
        [System.Web.Http.HttpPost]
        public AbstractModel ListOfWorkoutsName(LoginModel loginModel)
        {
            user user = findUser(loginModel.userName);
            if(user == null)
            {
                return ResultHelper.boolResult(false, "User not exist !!!");
            }
            return ResultHelper.workoutsRessult(loginModel.userName);
            //return ResultHelper.boolResult(true,"gfgf");
        }

        [System.Web.Http.HttpPost]
        public AbstractModel ListOfTaskName(WorkoutModel workoutModel)
        {
            //get workout by name
            workout _workout = findWorkout(workoutModel.workoutName);
            if (_workout == null)
            {
                return ResultHelper.boolResult(false, "Workout not exist !!!");
            }
            return ResultHelper.tasksRrssult(workoutModel.workoutName);
        }
        [System.Web.Http.HttpPost]
        public AbstractModel TaskByName(String taskName) 
        {
            // get task by name
            task _task = findTask(taskName);
            if (_task == null)
            {
                return ResultHelper.boolResult(false, "Task not exist !!!");
            }
            return ResultHelper.taskByNameResult(taskName);
        }
        #endregion

        #region
        private static user checkLogin(string userName, string password)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get user by userName
            user _user = db.users.Where(x => x.userName == userName).SingleOrDefault();
            db.Dispose();
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
            db.Dispose();
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
            db.Dispose();
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
            db.Dispose();
            return user;
        }

        //query function to check if workout exist in DB
        private static workout findWorkout(string workoutName)
        {
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get workout by workoutName
            workout _workout = db.workouts.Where(x => x.workoutName == workoutName).SingleOrDefault();
            db.Dispose();
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
            db.Dispose();
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

            db.SaveChanges();
            return true;
        }

        private static bool addWorkout(string userName,string workoutName)
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
            newWorkout.userName = userName;
            newWorkout.workoutName = workoutName;
            
            db.workouts.Add(newWorkout);
            db.SaveChanges();
            return true;
        }

        private static bool addTask(string workoutName ,string taskName, string descriptionTask, string timeTask, string revTask)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if task exist in DB
            task newTask = findTask(taskName);
            if (newTask != null)
            {
                return false;
            }
            
            newTask = new task();
            //if task not exist create new task
            newTask.workoutName = workoutName;
            newTask.taskName = taskName;
            newTask.description = descriptionTask;
            newTask.time = timeTask;
            newTask.rev = revTask;
            newTask.workout = null;

            db.tasks.Add(newTask);
            db.SaveChanges();
            return true;
        }

        //query function to add new user to DB
        private static bool changeProfile(string firstName, string lastName, string userName, string password)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if user exist in DB
            user user = findUser(userName);
            if (user == null)
            {
                return false;
            }
            //if user exist change user profile
            user.firstName = firstName;
            user.lastName = lastName;
            user.password = password; // CryptHelper.getHash(password);
           
            db.Entry(user).State = EntityState.Modified;

            db.SaveChanges();
            return true;
        }
        #endregion

        #region delete
        private static bool deleteWorkout(string userName, string workoutName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if workout exist in DB
            workout delWorkout = findWorkoutByUsername(userName , workoutName);
            task delTask = findTask(workoutName);
            if (delWorkout == null) // && delTask == null)
            {
                return false;
            }
            //if (delWorkout.userName == userName && delWorkout.workoutName == workoutName)// && delTask.workoutName == workoutName) 
            //{
                db.workouts.Remove(delWorkout);
                //db.tasks.Remove(delTask);
                db.SaveChanges();
                return true;
            //}

            //return false;
        }
        private static bool deleteTask(string workoutName, string taskName)
        {
            //connect to db
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();

            //check if task exist in DB
            task delTask = findTask(workoutName);
            if (delTask == null)
            {
                return false;
            }
            //if (delTask.workoutName == workoutName && delTask.taskName == taskName)
            //{
                db.tasks.Remove(delTask);
                db.SaveChanges();
                return true;
            //}

            //return false;
        }
        //query function to check if workout exist in DB
        private static workout findWorkoutByUsername(string _userName, string _workoutName)
        {
            social_workout_app_dbEntities db = new social_workout_app_dbEntities();
            //get workout by workoutName
            //workout _workout = db.workouts.Where(x => x.userName == userName).Select(s => s.workoutName == workoutName).SingleOrDefault();
            workout _workout = db.workouts.Where(x => x.workoutName == _workoutName && x.userName == _userName).SingleOrDefault();
            db.Dispose();
            //check if workout exist
            if (_workout == null)
            {
                return null;
            }
            return _workout;
        }
        #endregion
    }
}
