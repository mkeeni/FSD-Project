import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Transaction from '../../../../Transactions/Transaction/Transaction';
import { useSelector } from 'react-redux';

function CustomerTransactions(){
    var [transactions,setTransactions] = useState([]);
    var [error,setError] = useState(false);
    var navigate = useNavigate();

    var customerID = useSelector((state) => state.customerID);
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        if(customerID === 0){
            navigate("/employeeMenu/allCustomers");
        }
        else{
            allTransactions();
        }
    },[])

    async function allTransactions(){
        await axios.get('http://localhost:7289/api/Transactions/GetAllCustomerTransactions?customerID=' + customerID,httpHeader).then(function (response) {
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
            if(error.response.data === "No Transaction History Found for Customer ID " + customerID){
                setError(true);
            }
        })
    }

    return (
        <div>
            {error ? 
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">No Transaction History Found</div>
                </div> : 
                <div className="heigthBox2 scrolling">
                    {transactions.map(transaction =>
                        <Transaction key={transaction.transactionID} transaction={transaction}/> 
                    )}
                </div>
            }
        </div>
    );
}

export default CustomerTransactions;