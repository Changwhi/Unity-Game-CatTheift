const mySqlDatabase = include('databaseConnectionSQL');

async function createUser(postData) {
  let createUserSQL = `
    INSERT INTO user (hashed_password, username)
    VALUES (:passwordHash, :username);`;

  let params = {
    username: postData.username,
    passwordHash: postData.hashedPassword
  }

	try {
		const results = await mySqlDatabase.query(createUserSQL, params);
        console.log("Successfully created user");
		return true;
	}
	catch(err) {
        console.log("Error inserting user");
        console.log(err);
		return false;
	}
}

async function getUsers() {
	let getUsersSQL = `
		SELECT hashed_password, user_id, username
		FROM user;
	`;

  try {
    const results = await mySqlDatabase.query(getUsersSQL);
    return results[0];
  }
  catch (err) {
    console.log("Error getting users");
    console.log(err);
    return false;
  }
}



module.exports = { createUser, getUsers };
