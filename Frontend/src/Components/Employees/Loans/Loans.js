import axios from 'axios';
import { useEffect, useState } from "react";
import '../../style.css';
import { Outlet, useNavigate } from "react-router-dom";
import LoanApplication from './LoanApplication/LoanApplication';

function Loans(){

    var [loans,setLoans] = useState([]);
    var [error,setError] = useState(false);
    var [errorMessage,setErrorMessage] = useState("");

    useEffect(() => {
        getAllPendingAppliedLoans();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAllPendingAppliedLoans(){
        await axios.get('http://localhost:7289/api/AppliedLoans/GetAllAppliedLoansStatus?status=Pending',httpHeader)
        .then(function (response) {
            setLoans(response.data);
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <span className="textDecoOrange margin3">Loan Applications</span>
                {error ? 
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> :
                <div className="scrolling phoneBox3">
                    {loans.map(loan => 
                        <LoanApplication key={loan.loanApplicationID} loan = {loan}/>
                    )}
                </div>}
            </div>
        </div>
    );
}

export default Loans;