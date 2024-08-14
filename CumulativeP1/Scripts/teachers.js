// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:61978/api/TeacherData/AddTeacher
	//with POST data of fname, lname employeeNum, hire date, salary, etc.

	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	var URL = "http://localhost:61978/api/TeacherData/AddTeacher/";
	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;

	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"TeacherSalary": TeacherSalary,
		"EmployeeNumber": EmployeeNumber,
		"TeacherHireDate": TeacherHireDate
	};
	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.
			console.log(TeacherData);
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}


// Updated on 0812
function UpdateTeacher(TeacherId) {

	//check for validation straight away
	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:61978/api/TeacherData/EditTeacher/{id}
	//with POST data of teacherfname, teacherlname, employeenumber, salary and hiredate

	var URL = "http://localhost:61978/api/TeacherData/EditTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;

	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"TeacherSalary": TeacherSalary,
		"EmployeeNumber": EmployeeNumber,
		"TeacherHireDate": TeacherHireDate
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}



// Usually Validation functions for Add and Update are separated.
// You can run into situations where information added is no longer updated, or vice versa
// However, as an example, validation is consolidated into 'ValidateTeacher'
// This is so that both Ajax and Non Ajax techniques can utilize the same client-side validation logic.
function ValidateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var TeacherSalary = document.getElementById('TeacherSalary').value;
	var TeacherHireDate = document.getElementById('TeacherHireDate').value;

	//First Name is two or more characters
	if (TeacherFname.length < 2) {
		IsValid = false;
		ErrorMsg += "First Name Must be 2 or more characters.<br>";
	}
	//Last Name is two or more characters
	if (TeacherLname.length < 2) {
		IsValid = false;
		ErrorMsg += "Last Name Must be 2 or more characters.<br>";
	}
	//TeacherHireDate is valid
	let minDate = new DateTime(1900, 1, 1);
    let maxDate = DateTime.Now;
	if (TeacherHireDate < minDate || TeacherHireDate > maxDate) {
		IsValid = false;
		ErrorMsg += "Please select a hire date.<br>";
	}


	//EmployeeNumber is valid pattern
	if (!employeeNumRegEx(EmployeeNumber)) {
		IsValid = false;
		ErrorMsg += "Employee Number starts with the letter 'T' followed by three digits.<br>";
	}
	//TeacherSalary is valid pattern
	if (!salaryRegEx(TeacherSalary)) {
		IsValid = false;
		ErrorMsg += "Valid salary is a number less than 100.<br>";
	}

	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}


	return IsValid;
}


function employeeNumRegEx(EmployeeNumber) {
	let employeeNumRegEx = /^T\d{3}$/;
	return employeeNumRegEx.test(EmployeeNumber.value);
}
// Valid salary is a number less than 100.
function salaryRegEx(TeacherSalary) {
	let salaryRegEx = /^\d{2}(\.\d{1,2})?$/;
	return salaryRegEx.test(TeacherSalary.value);
}


// This function attaches a timer object to the input window.
// When the timer expires (300ms), the search executes.
// Prevents a search on each key up for fast typers.
function _ListTeachers(d) {

	if (d.timer) clearTimeout(d.timer);
	d.timer = setTimeout(function () { ListTeachers(d.value); }, 300);
}

//The actual List Teachers Method.
function ListTeachers(SearchKey) {

	var URL = "http://localhost:61978/api/TeacherData/ListTeachers/" + SearchKey;

	var rq = new XMLHttpRequest();
	rq.open("GET", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished


			var teachers = JSON.parse(rq.responseText)
			var listteachers = document.getElementById("listteachers");
			listteachers.innerHTML = "";

			//renders content for each teacher pulled from the API call
			for (var i = 0; i < teachers.length; i++) {
				var row = document.createElement("div");
				row.classList = "listitem row";
				var col = document.createElement("col");
				col.classList = "col-md-12";
				var link = document.createElement("a");
				link.href = "/Teacher/Show/" + teachers[i].TeacherId;
				link.innerHTML = teachers[i].TeacherFname + " " + teachers[i].TeacherLname;

				col.appendChild(link);
				row.appendChild(col);
				listteachers.appendChild(row);

			}
		}

	}
	//POST information sent through the .send() method
	rq.send();
}