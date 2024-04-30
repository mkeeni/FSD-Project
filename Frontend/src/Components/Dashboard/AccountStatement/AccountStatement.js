import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import Transaction from '../../Transactions/Transaction/Transaction';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

function AccountStatement() {

    var [accountStatement,setAccountStatement] = useState({});
    var [transactions,setTransactions] = useState([]);
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var navigate = useNavigate();

    var statementData = useSelector((state) => state.statement);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        transactionReport();
    },[])

    async function transactionReport(){
        if(statementData.accountID=== 0){
            navigate("/menu/dashboard");
        }
        else{
            await axios.get('http://localhost:7289/api/Transactions/GetTransactionsBetweenTwoDates?accountID=' + statementData.accountID +'&fromDate=' + statementData.fromDate +'&toDate=' + statementData.toDate,httpHeader).then(function (response) {
                setError(false);
                setTransactions(response.data);
                generateReport()
            })
            .catch(function (error) {
                console.log(error);
                setError(true);
                setErrorMessage(error.response.data);
            })
        } 
    }

    async function generateReport(){
        await axios.get('http://localhost:7289/api/Transactions/GetAccountStatement?accountID=' + statementData.accountID +'&fromDate=' + statementData.fromDate +'&toDate=' + statementData.toDate,httpHeader).then(function (response) {
            setAccountStatement(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            {error ? 
                        <div>
                            <Link to = "/menu/dashboard">
                                <div className="leftArrow change-my-color margin3"></div>
                            </Link>
                            <div className="smallBox66">
                                <div className="errorImage2 change-my-color2"></div>
                                <div className="clickRegisterText">{errorMessage}</div>
                            </div>
                        </div>
                         : 
                        <div className="smallBox40">
                        <Link to = "/menu/dashboard">
                            <div className="leftArrow change-my-color margin3"></div>
                        </Link>
                        <span className="clickRegisterText10">Account Statement</span>
                        <div className="flexRow2">
                            <div className="smallBox32">
                                <span className="clickRegisterTextBlack">Balance</span>
                                <span className="clickRegisterText3">{accountStatement.balance}</span>
                            </div>
                            <div className="smallBox32">
                                <span className="clickRegisterTextBlack">Deposits</span>
                                <span className="clickRegisterText3">{accountStatement.totalDeposit}</span>
                            </div>
                            <div className="smallBox32">
                                <span className="clickRegisterTextBlack">Withdrawal</span>
                                <span className="clickRegisterText3">{accountStatement.totalWithdrawal}</span>
                            </div>
                        </div>
                        <hr className="hrS"></hr>
                        <span className="clickRegisterText10">Total Transactions</span>
                        <div className="scrolling phoneBox">
                                    {transactions.map(transaction =>
                                    <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                                    )
                                    }
                                </div>
                    </div>
                    }
        </div>
    );
}

export default AccountStatement;