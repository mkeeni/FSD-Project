import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CustomerAppliedLoans(props) {

    var [availedLoans,setAvailedLoans] = useState([]);
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    useEffect(() => {
        getCustomerAppliedLoans();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerAppliedLoans() {
        await axios.get('http://localhost:7289/api/AppliedLoans/GetAllCustomerAppliedLoans?customerID=' + props.loan.customerID, httpHeader)
            .then(function (response) {
                setAvailedLoans(response.data);
                setError(false);
            })
            .catch(function (error) {
                console.log(error);
                setError(true);
                setErrorMessage(error.response.data);
            })
    }

    return (
        <div className="scrolling phoneBox">
            <div>
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterTextBlack">{errorMessage}</div>
                </div> : 
                <div>
                    {availedLoans.map((availedLoan) => 
                        <div key = {availedLoan.loanApplicationID} className="whiteOutlineBox4">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Applied Loan Amount: {availedLoan.amount}</span>
                                    <span className="clickRegisterText4">{availedLoan.status}</span>
                                </div>
                                <span className="clickRegisterText">Interest: {availedLoan.loans.interest}</span>
                                <span className="clickRegisterText">Tenure: {availedLoan.loans.tenure} yrs</span>
                                <span className="clickRegisterText">Loan Type: {availedLoan.loans.loanType}</span>
                                <span className="clickRegisterText">Purpose: {availedLoan.purpose}</span>
                                <span className="clickRegisterText">Applied Date: {availedLoan.appliedDate}</span>
                            </div>
                        </div>
                    )}
                </div>}
            </div>   
        </div>
    );
}

export default CustomerAppliedLoans;