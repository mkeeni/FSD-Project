import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../../style.css';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';


function InboudsOutbounds() {

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var accountID = useSelector((state) => state.accountID);
    var navigate = useNavigate();

    var [accountReport,setAccountReport] = useState({});

    useEffect(() => {
        getAccountReport();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAccountReport(){
        if(accountID === 0){
            navigate("/employeeMenu/accounts/viewCustomerAccount");
        }
        else{
            await axios.get('http://localhost:7289/api/Transactions/AccountFinancialPerformanceReport?accountID=' + accountID,httpHeader)
            .then(function (response) {
                setAccountReport(response.data);
                setError(false);
            })
            .catch(function (error) {
                console.log(error);
                setError(true);
                setErrorMessage(error.response.data);
            })
        }
    }

    return (
        <div>
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> : 
                <div className="smallBox40 margin4 widthBox">
                    <span className="clickRegisterText7">Total Transactions: {accountReport.totalTransactions}</span>
                    <span className="clickRegisterText7">Inbound Transactions: {accountReport.inboundTransactions}</span>
                    <span className="clickRegisterText7">Outbound Transactions: {accountReport.outboundTransactions}</span>
                    <span className="clickRegisterText7">Ratio: {accountReport.ratio}</span>
                    <span className="clickRegisterText7">CreditWorthiness: {accountReport.creditWorthiness}</span>
                </div>}
            </div>
    );
}

export default InboudsOutbounds;