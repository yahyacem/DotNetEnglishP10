db = new Mongo().getDB("mediscreen-db");
db.createCollection('Patients', { capped: false });
db.createCollection('History', { capped: false });
db.createCollection('TriggerTerms', { capped: false });

let patientsResult = db.Patients.insert([
    {
        "GivenName": "TestNone",
        "FamilyName": "Test",
        "DateOfBirth": new Date('1966-12-31'),
        "Sex": "F",
        "HomeAddress": "1 Brookside St",
        "PhoneNumber": "100-222-3333"
    },
    {
        "GivenName": "TestBorderline",
        "FamilyName": "Test",
        "DateOfBirth": new Date('1945-06-24'),
        "Sex": "M",
        "HomeAddress": "2 High St",
        "PhoneNumber": "200-333-4444"
    },
    {
        "GivenName": "TestInDanger",
        "FamilyName": "Test",
        "DateOfBirth": new Date('2004-06-18'),
        "Sex": "M",
        "HomeAddress": "3 Club Road",
        "PhoneNumber": "300-444-5555"
    },
    {
        "GivenName": "TestEarlyOnset",
        "FamilyName": "Test",
        "DateOfBirth": new Date('2002-06-28'),
        "Sex": "F",
        "HomeAddress": "4 Valley Dr",
        "PhoneNumber": "400-555-6666"
    }
]);

db.History.insert([
    {
        "PatientId": patientsResult.insertedIds[0].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they are 'feeling terrific' Weight at or below recommended level"
    },
    {
        "PatientId": patientsResult.insertedIds[1].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they are feeling a great deal of stress at work\n"
            + "Patient also complains that their hearing seems Abnormal as of late"
    },
    {
        "PatientId": patientsResult.insertedIds[1].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they have had a Reaction to medication within last 3 months\n"
            + "Patient also complains that their hearing continues to be problematic"
    },
    {
        "PatientId": patientsResult.insertedIds[2].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they are short term Smoker"
    },
    {
        "PatientId": patientsResult.insertedIds[2].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they quit within last year\n"
            + "Patient also complains that of Abnormal breathing spells Lab reports Cholesterol LDL high"
    },
    {
        "PatientId": patientsResult.insertedIds[3].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that walking up stairs has become difficult\n"
            + "Patient also complains that they are having shortness of breath Lab results indicate Antibodies present elevated Reaction to medication"
    },
    {
        "PatientId": patientsResult.insertedIds[3].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they are experiencing back pain when seated"
    },
    {
        "PatientId": patientsResult.insertedIds[3].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that they are a short term Smoker Hemoglobin A1C above recommended level"
    },
    {
        "PatientId": patientsResult.insertedIds[3].toString(),
        "CreationDate": new Date(Date.now()),
        "NotesRecommendations": "Patient states that Body Height, Body Weight, Cholesterol, Dizziness and Reaction"
    }
]);

db.TriggerTerms.insert([
    { "Term": "Hemoglobin A1C" },
    { "Term": "Microalbumin" },
    { "Term": "Body Height" },
    { "Term": "Body Weight" },
    { "Term": "Smoker" },
    { "Term": "Abnormal" },
    { "Term": "Cholesterol" },
    { "Term": "Dizziness" },
    { "Term": "Relapse" },
    { "Term": "Reaction" },
    { "Term": "Antibodies" }
]);