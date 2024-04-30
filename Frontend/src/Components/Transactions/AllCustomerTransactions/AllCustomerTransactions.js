import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Transaction from '../Transaction/Transaction';

function AllCustomerTransactions(){

    var [transactions,setTransactions] = useState([]);
    var [error,setError] = useState(false);
    var [errorMessage,setErrorMessage] = useState("");

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allTransactions();
    },[])

    async function allTransactions(){
        await axios.get('http://localhost:7289/api/Transactions/GetAllCustomerTransactions?customerID=' + customerID,httpHeader).then(function (response) {
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox43">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoBlack smallBox23" to="/menu/customerTransactions">History</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/menu/transferMoney">Transfer Money</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoOrange smallBox23" to="/menu/depositMoney">Deposit</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoOrange smallBox23" to="/menu/withdrawMoney">Withdraw</Link>
                        </li>
                    </ul>
                    {error ? 
                        <div className="smallBox48">
                            <div className="errorImage2 change-my-color2"></div>
                            <div className="clickRegisterTextBlack">{errorMessage}</div>
                        </div> : 
                        <div className="scrolling">
                            {transactions.map(transaction =>
                            <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                            )
                            }
                        </div>
                    }
                </div>
        </div>
    );
}

export default AllCustomerTransactions;