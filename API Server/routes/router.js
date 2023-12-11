const router = require("express").Router();
const MongoStore = require("connect-mongo");
const session = require("express-session");
const bcrypt = require("bcrypt");
require("dotenv").config();
const Joi = require("joi");
const db_users = include('database/users');

const saltRounds = 12;
const expireTime = 60 * 60 * 1000;

const mongodb_user = process.env.MONGODB_USER;
const mongodb_password = process.env.MONGODB_PASSWORD;
const mongodb_session_secret = process.env.MONGODB_SESSION_SECRET;
const node_session_secret = process.env.NODE_SESSION_SECRET;

const passwordSchema = Joi.object({
  password: Joi.string().pattern(/(?=.*[a-z])/).pattern(/(?=.*[A-Z])/).pattern(/(?=.*[!@#$%^&*])/).pattern(/(?=.*[0-9])/).min(12).max(50).required()
});


var mongoStore = MongoStore.create({
  mongoUrl: `mongodb+srv://${mongodb_user}:${mongodb_password}@cluster1.5f9ckjd.mongodb.net/COMP4921_Project1_DB?retryWrites=true&w=majority`,
  crypto: {
    secret: mongodb_session_secret,
  },
});

router.use(
  session({
    secret: node_session_secret,
    store: mongoStore,
    saveUninitialized: false,
    resave: true,
  })
);


router.get("/", async (req, res) => {
  console.log("idex page hit")
  res.send("Hello World!")
});


router.post("/login", async (req, res) => {
  var username = req.body.username;
  var password = req.body.password;
  var users = await db_users.getUsers();
  let user = null;

  for (let i = 0; i < users.length; i++) {
    if (users[i].username == username) {
      user = users[i];
      break;
    }
  }

  if (!user) {
    res.status(400).send("Invalid username or password");
    return;
  }

  const isValidPassword = bcrypt.compareSync(password, user.hashed_password);
  if (isValidPassword) {
    console.log("User's logged in")
    req.session.userID = user.user_id;
    req.session.name = user.name
    req.session.authenticated = true;
    req.session.cookie.maxAge = expireTime;
    res.status(200).send("Login successful");
  } else {
    req.session.userID = null;
    req.session.name = null;
    req.session.authenticated = false;
    res.status(400).send("Failed to log in");
  }
});


router.post("/signup", async (req, res) => {
  var username = req.body.username;
  var password = req.body.password;
  var hashedPassword = bcrypt.hashSync(password, saltRounds);

  const validationResult = passwordSchema.validate({
    password
  });

  if (validationResult.error) {
    let errorMsg = validationResult.error.details[0].message;

    if (errorMsg.includes("(?=.*[a-z])")) {
      errorMsg = "Password must have at least 1 lowercase.";
    } else if (errorMsg.includes("(?=.*[A-Z])")) {
      errorMsg = "Password must have at least 1 uppercase.";
    } else if (errorMsg.includes("(?=[!@#$%^&*])")) {
      errorMsg = "Password requires 1 special character.";
    } else if (errorMsg.includes("(?=.*[0-9])")) {
      errorMsg = "Password needs to have 1 number.";
    }
    res.status(400).send(`${errorMsg}`);
    return;
  } else {
    var success = await db_users.createUser({
      username: username,
      hashedPassword: hashedPassword,
    });

    if (success) {
      res.status(201).send("Successful sign up!")
    } else {
      res.status(400).send(`Failed to create the user ${username}`);
    }
  }
});

router.get('/logout', (req, res) => {
  console.log("Logging out");

  req.session.destroy(err => {
    if (err) {
      console.error('Error destroying session:', err);
      return res.status(500).send('Failed to log out');
    }

    res.redirect('/login');
  });
});


module.exports = router;