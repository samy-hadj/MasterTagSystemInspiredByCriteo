// init.js
try {
    // Drop existing database if it exists
    db = db.getSiblingDB('CriteoProject');
    db.dropDatabase();
    console.log("Database CriteoProject dropped successfully");

    // Create fresh database and collection
    db = db.getSiblingDB('CriteoProject');
    db.createCollection('jsons');
    console.log("Fresh database and collection created successfully");

} catch (error) {
    console.error("Error during database initialization:", error);
}