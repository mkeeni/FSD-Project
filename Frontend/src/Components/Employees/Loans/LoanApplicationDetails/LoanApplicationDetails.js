import axios from 'axios';
import { useEffect, useState } from "react";
import { useSelector } from 'react-redux';
import '../../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import ApplicantDetails from './ApplicantDetails/ApplicantDetails';
import CreditWorthiness from './CreditWorthiness/CreditWorthiness';
import CustomerAppliedLoans from './CustomerAppliedLoans/CustomerAppliedLoans';

function LoanApplicationDetails(){

    var [clicked,setClicked] = useState([true,false,false]);
    var loanID = useSelector((state) => state.loanID);
    var [successMessage,setSuccessMessage]= useState("");
    var navigate = useNavigate();

    var [loan,setLoan] = useState(
        {
            "loanApplicationID": 0,
            "amount": 0,
            "purpose": "string",
            "status": "string",
            "appliedDate": "2024-02-28T11:26:05.497Z",
            "loanID": 0,
            "loans": {
              "loanID": 0,
              "loanAmount": 0,
              "loanType": "string",
              "interest": 0,
              "tenure": 0
            },
            "customerID": 0,
            "customers": {
              "customerID": 0,
              "name": "string",
              "dob": "2024-02-28T11:26:05.497Z",
              "age": 0,
              "phoneNumber": 0,
              "address": "string",
              "aadharNumber": 0,
              "panNumber": "string",
              "gender": "string",
              "email": "string",
              "validation": {
                "email": "string",
                "password": "string",
                "userType": "string",
                "key": "string"
              }
            }
          }
    );

    useEffect(() => {
        getAppliedLoan();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAppliedLoan(){
        if(loanID === 0){
            navigate("/employeeMenu/loans");
        }
        else{
            await axios.get('http://localhost:7289/api/AppliedLoans/GetAppliedLoan?loanApplicationID=' + loanID,httpHeader)
            .then(function (response) {
                setLoan(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function approveLoan(){
        await axios.put('http://localhost:7289/api/AppliedLoans/UpdateAppliedLoanStatus?loanApplicationID=' + loanID + '&status=Approved',loan,httpHeader)
        .then(function (response) {
            setSuccessMessage("Loan Application Approved");
            showToast();
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function disApproveLoan(){
        await axios.put('http://localhost:7289/api/AppliedLoans/UpdateAppliedLoanStatus?loanApplicationID=' + loanID + '&status=Disapproved',loan,httpHeader)
        .then(function (response) {
            setSuccessMessage("Loan Application Disapproved");
            showToast();
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function setApplicantDetails(){
        setClicked([true,false,false]);
    }

    function setCreditWorthiness(){
        setClicked([false,true,false]);
    }

    function setCustomerAppliedLoans(){
        setClicked([false,false,true]);
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return(
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
            <div className="upMargin3">
                <Link to="/employeeMenu/loans">
                    <div className="leftArrow change-my-color"></div>
                </Link> 
                <div className='flexRow'>
                    <div className="smallBox53">
                        <span className="btn btn-outline-danger pointer" data-bs-toggle="modal" data-bs-target="#modal3">Disapprove</span>
                    </div>
                    <div className="smallBox53">
                        <span className="btn btn-outline-success pointer" data-bs-toggle="modal" data-bs-target="#modal1">Approve</span>
                    </div>
                </div>
            </div>
            <span className="clickRegisterText9">Loan Details</span>
            <span className="clickRegisterText7">Loan Amount: Rs.{loan.loans.loanAmount} -- Interest: {loan.loans.interest} %</span>
            <span className="clickRegisterText7">Tenure: {loan.loans.tenure} yrs -- Loan Type: {loan.loans.loanType}</span>
            <hr className='hrS'></hr>
            <ul className="smallBox22 nav">
                <li className="nav-item highlight smallBox23">
                            {clicked[0] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={setApplicantDetails}>Applicant Details</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={setApplicantDetails}>Applicant Details</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[1] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={setCreditWorthiness}>CreditWorthiness</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={setCreditWorthiness}>CreditWorthiness</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[2] ? <span className="nav-link textDecoBlack pointer smallBox23" onClick={setCustomerAppliedLoans}>Applied Loans</span> :
                            <span className="nav-link textDecoOrange pointer smallBox23" onClick={setCustomerAppliedLoans}>Applied Loans</span>}
                        </li>
            </ul>
            {clicked[0] ? <ApplicantDetails loan = {loan}/> : null}
            {clicked[1] ? <CreditWorthiness loan = {loan}/> : null}
            {clicked[2] ? <CustomerAppliedLoans loan = {loan}/> : null}
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Close Loan</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to Approve this Loan Application?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={approveLoan}>Approve</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="modal fade" id="modal3" tabIndex="-1" aria-labelledby="modalEg2" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg2">Disapprove Close Request</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to DisApprove this Loan Application?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={disApproveLoan}>Disapprove</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        {successMessage}
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                </div>
        </div>
    );
}

export default LoanApplicationDetails;