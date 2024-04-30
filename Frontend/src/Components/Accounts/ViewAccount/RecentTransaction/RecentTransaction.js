import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import Transaction from '../../../Transactions/Transaction/Transaction';

function RecentTransaction(){

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var accountID = useSelector((state) => state.accountID);
    var [transactions,setTransactions] = useState([]);
    var navigate = useNavigate();

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        if(accountID === 0){
            navigate("/menu/customerAccounts");
        }
        else{
            getRecentTransactions();
        }
    },[])

    async function getRecentTransactions(){
        await axios.get('http://localhost:7289/api/Transactions/GetRecentTenAccountTransactions?accountID=' + accountID,httpHeader)
        .then(function (response) {
            setTransactions(response.data);
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    return(
        <div className="scrolling phoneBox2">
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterTextBlack">{errorMessage}</div>
                </div> : 
                <div>
                    {transactions.map(transaction => 
                        <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                    )}
                </div>
            }
        </div> 
    );
}

export default RecentTransaction;