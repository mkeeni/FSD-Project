import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

function ApplyLoan(){

    var loanID = useSelector((state) => state.loanID);
    const customerID = sessionStorage.getItem('id');
    var [amount,setAmount] = useState("");
    var [purpose,setPurpose] = useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var [loan,setloan] = useState(
        {
            "loanID": 0,
            "loanAmount": "",
            "loanType": "",
            "interest": "",
            "tenure": "",
        }
    );

    var newLoan = {
        "amount": amount,
        "purpose": purpose,
        "loanID": loanID,
        "customerID": customerID 
    }

    var navigate = useNavigate();

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allLoans();
    },[])

    async function allLoans(){
        if(loanID === 0){
            navigate("/menu/allLoans")
        }
        else{
            await axios.get('http://localhost:7289/api/Loans/GetLoan?loanID=' + loanID,httpHeader)
            .then(function (response) {
                setloan(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function applyForLoan(){
        if(amount === "" || purpose === ""){
            alert("Please fill in all the details");
        }
        else{
            if(amount > 0){
                if(purpose.length > 3 &&purpose.length < 100){
                    await axios.post('http://localhost:7289/api/AppliedLoans/AddAppliedLoan', newLoan, httpHeader)
                    .then(function (response) {
                        setError(false);
                        showToast();
                    })
                    .catch(function (error) {
                        console.log(error);
                        setError(true);
                        setErrorMessage(error.response.data);
                    })
                }
                else{
                    alert("Purpose field should be between 4 and 100 characters long");
                }
            }
            else{
                alert("Please enter a valid amount");
            }
        }
    }

    function amountValidation(eventargs){
        var amount = eventargs.target.value;
        setAmount(amount);
        if(amount !== ""){
            if(amount > 0){
                setError(false);
                if(amount !== "" && purpose !== ""){
                    document.getElementById("applyLoan").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Invalid Amount Entered");
                document.getElementById("applyLoan").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter an Amount");
            document.getElementById("applyLoan").classList.add("disabled");
        }
    }

    function purposeValidation(eventargs){
        var purpose = eventargs.target.value;
        setPurpose(purpose);
        if(purpose !== ""){
            if(purpose.length > 3 && purpose.length < 100){
                setError(false);
                if(amount !== "" && purpose !== ""){
                    document.getElementById("applyLoan").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Purpose field should be between 4 and 100 characters long");
                document.getElementById("applyLoan").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter a Purpose");
            document.getElementById("applyLoan").classList.add("disabled");
        }
    }

    function showToast(){
        document.querySelector('.toast').classList.add('show');
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox28">
                    <div className="upMargin">
                        <Link to="/menu/allLoans">
                            <div className="leftArrow change-my-color"></div>
                        </Link >
                    </div>
                    <span className="clickRegisterTextBlack">Loan Amount: {loan.loanAmount}</span>
                    <span className="clickRegisterTextBlack">Interest: {loan.interest}</span>
                    <span className="clickRegisterTextBlack">Tenure: {loan.tenure}</span>
                    <span className="clickRegisterTextBlack">Loan Type: {loan.loanType}</span>
                    <div>
                        <span className="clickRegisterTextBlack">Your Amount</span>
                        <input className="form-control enterDiv3" type="number" onChange={amountValidation}></input>
                    </div>
                    <div>
                        <span className="clickRegisterTextBlack">Purpose</span>
                        <textarea className="form-control enterDiv4" type="text" onChange={purposeValidation}></textarea>
                    </div>
                    {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                    <a id="applyLoan" className="btn btn-outline-success smallBox9 disabled" href="applyLoan.html" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Apply</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Apply Loan</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to apply for this Loan?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={applyForLoan}>Apply</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
                    <div className="d-flex">
                    <div className="toast-body">
                        Loan Application Successful, Please wait for approval
                    </div>
                    <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
            </div>
        </div>
    );
}

export default ApplyLoan;