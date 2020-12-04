function addTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:51930/api/TeacherData/AddTeacher
	//with POST data of a teacher

	var URL = "http://localhost:51930/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var TeacherNumber = document.getElementById('TeacherNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;



	var TeacherData = {
		"firstname": TeacherFname,
		"lastname": TeacherLname,
		"employeeNumber": TeacherNumber,
		"salary": Number.parseFloat(TeacherSalary)
	};
	console.log(TeacherData);

	// ajax query
	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 204) {
			//request is successful and the request is finished
			//return to the list of teachers page
			window.location.replace("http://localhost:51930/Teacher/List");
		}

	}
	console.log(JSON.stringify(TeacherData));
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}

function deleteTeacher() {
	// get the teacher's id and name 
	var teacherId = document.getElementById("teacherid").value;
	var teacherName = document.getElementById("fname").innerText + " " + document.getElementById("lname").innerText;
	//request a confirmation of the action b
	var confirmAction = confirm("Do you want to delete teacher " + teacherName);
	// if the action is confirmed delete the teacher
	if (confirmAction) {
		var URL = "http://localhost:51930/api/TeacherData/DeleteTeacher/" + teacherId;
		
		var rq = new XMLHttpRequest();
		// ajax query
		rq.open("POST", URL, true);
		rq.onreadystatechange = function () {
			//ready state should be 4 AND status should be 204
			if (rq.readyState == 4 && rq.status == 204) {
				//request is successful and the request is finished
				//return to the list of teachers page
				window.location.replace("http://localhost:51930/Teacher/List");



			}

		}
		//POST information sent through the .send() method
		rq.send();
    }
}

window.onload = function () {
	if (document.getElementById("btnCreate") !== null) {
		document.getElementById("btnCreate").onclick = addTeacher;
	}
	if (document.getElementById("link-delete-teacher") !== null) {
		document.getElementById("link-delete-teacher").onclick = deleteTeacher;
	}

}