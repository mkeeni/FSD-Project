import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CreditWorthiness(props) {

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [report, setReport] = useState({});

    useEffect(() => {
        getCustomerReport();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerReport() {
        await axios.get('http://localhost:7289/api/Transactions/CustomerRegulatoryReport?customerID=' + props.loan.customerID, httpHeader)
            .then(function (response) {
                setReport(response.data);
                setError(false);
            })
            .catch(function (error) {
                console.log(error);
                setError(true);
                setErrorMessage(error.response.data);
            })
    }

    return (
        <div>
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> : 
                <div className='smallBox40 margin4'>
                    <span className="clickRegisterText7">Total Transactions: {report.totalTransactions}</span>
                    <span className="clickRegisterText7">Inbound Transactions: {report.inboundTransactions}</span>
                    <span className="clickRegisterText7">Outbound Transactions: {report.outboundTransactions}</span>
                    <span className="clickRegisterText7">Ratio: {report.ratio}</span>
                    <span className="clickRegisterText7">CreditWorthiness: {report.creditWorthiness}</span>
                </div>}
        </div>
    );
}

export default CreditWorthiness;