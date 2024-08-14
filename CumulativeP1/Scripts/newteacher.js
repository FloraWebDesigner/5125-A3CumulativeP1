



/* my codes below                  */
// window.onload = pageReady;


function pageReady() {
	let TeacherFname = document.getElementById("TeacherFname");
    let TeacherLname = document.getElementById("TeacherLname");
    let EmployeeNumber = document.getElementById("EmployeeNumber");
    let TeacherSalary = document.getElementById("TeacherSalary");
    let TeacherHireDate = document.getElementById("TeacherHireDate");
    let btn=document.getElementById("AddTeacher");

    let fnameRequired = document.getElementById("fnameRequired");
    let lnameRequired = document.getElementById("lnameRequired");
    let employeeNumRequired = document.getElementById("employeeNumRequired");
    let salaryRequired = document.getElementById("salaryRequired");
    let hireDateRequired = document.getElementById("hireDateRequired");

    let isValid=true;

    // Employee Number starts with the letter 'T' followed by three digits.
    function employeeNumRegEx(EmployeeNumber) {
		let employeeNumRegEx = /^T\d{3}$/;
		return employeeNumRegEx.test(EmployeeNumber.value);
	}
    // Valid salary is a number less than 100.
    function salaryRegEx(TeacherSalary) {
		let salaryRegEx = /^\d{2}(\.\d{1,2})?$/;
		return salaryRegEx.test(TeacherSalary.value);
	}

    function formValidation(){
        // validate first name
        if (TeacherFname.value === "") {
			TeacherFname.style.border = "2px solid red";
			fnameRequired.style.display = "block";
			TeacherFname.focus();
            isValid= false;
		}
		else {
			TeacherFname.style.border = "1px solid grey";
			fnameRequired.style.display = "none";
        }
        // validate last name
        if (TeacherLname.value === "") {
			TeacherLname.style.border = "2px solid red";
			lnameRequired.style.display = "block";
			TeacherLname.focus();
            isValid= false;
		}
		else {
			TeacherLname.style.border = "1px solid grey";
			lnameRequired.style.display = "none";
        }
        // validate employee number        
        if (EmployeeNumber.value === "" || employeeNumRegEx(EmployeeNumber) === false) {
			EmployeeNumber.style.border = "2px solid red";
			employeeNumRequired.style.display = "block";
			EmployeeNumber.focus();
            isValid= false;
		}
		else {
			EmployeeNumber.style.border = "1px solid grey";
			employeeNumRequired.style.display = "none";
        }
        // validate salary
        if (TeacherSalary.value === "" || salaryRegEx(TeacherSalary) === false) {
			TeacherSalary.style.border = "2px solid red";
			salaryRequired.style.display = "block";
			TeacherSalary.focus();
            isValid= false;
		}
		else {
			TeacherSalary.style.border = "1px solid grey";
			salaryRequired.style.display = "none";
        }
        // validate hire date
        if (TeacherHireDate.value === "") {
			TeacherHireDate.style.border = "2px solid red";
			hireDateRequired.style.display = "block";
			TeacherHireDate.focus();
            isValid= false;
		}
		else {
			TeacherHireDate.style.border = "1px solid grey";
			hireDateRequired.style.display = "none";
        }
        return isValid;
    }
        function onSubmit(event){
            if(!formValidation()){
                event.preventDefault(event);
            }
        }
   // btn.onsubmit=onSubmit;
       

}