import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet } from 'react-router-dom';
import Transaction from '../../../Transactions/Transaction/Transaction';
import { useDispatch, useSelector } from 'react-redux';
import { updateDate } from '../../../../dateSlice';

function FilterTransaction(){

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var accountID = useSelector((state) => state.accountID);
    var date = useSelector((state) => state.date);
    var [fromDate,setFromDate] = useState(date.fromDate);
    var [toDate,setToDate] = useState(date.toDate);
    var [transactions,setTransactions] = useState(
        [{
            "transactionID": 0,
            "amount": 0,
            "transactionType": "",
        }]
    );
    var [getTransactions,setGetTransactions] = useState(false);
    var dispatch = useDispatch();

    useEffect(() => {
        if(fromDate !== "" && toDate !== ""){
            getTransactionsBetweenTwoDates();
        }
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getTransactionsBetweenTwoDates(){
        dispatch(
            updateDate({
                "fromDate": fromDate,
                "toDate": toDate
            })
        )
        await axios.get('http://localhost:7289/api/Transactions/GetTransactionsBetweenTwoDates?accountID=' + accountID +'&fromDate=' + fromDate +'&toDate=' + toDate,httpHeader)
        .then(function (response) {
            setGetTransactions(true);
            setError(false);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setGetTransactions(true);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    function fromDateValidation(eventargs){
        var fromDate = eventargs.target.value;
        setFromDate(fromDate);
        if(fromDate !== "" && toDate !== ""){
            document.getElementById("generateButton").classList.remove("disabled");
        }
        else{
            document.getElementById("generateButton").classList.add("disabled");
        }
    }

    function toDateValidation(eventargs){
        var toDate = eventargs.target.value;
        setToDate(toDate);
        if(fromDate !== "" && toDate !== ""){
            document.getElementById("generateButton").classList.remove("disabled");
        }
        else{
            document.getElementById("generateButton").classList.add("disabled");
        }
    }

    function getFilter(){
        setGetTransactions(false);
        setFromDate("");
        setToDate("");
        dispatch(
            updateDate({
                "fromDate": "",
                "toDate": ""
            })
        )
    }

    return (
        <div>
            {getTransactions === true ? 
            <div className="heigthBox">
                <span className="btn btn-outline-success pointer margin5" onClick={getFilter}>
                    <span>Filter Again</span>
                </span>
                {error ? 
                <div className="smallBox65">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterTextBlack">{errorMessage}</div>
                </div> : 
                <div className="scrolling">
                    {transactions.map(transaction => 
                        <Transaction key={transaction.transactionID} transaction = {transaction}/>
                    )}
                </div>}
            </div> :
            <div className='phoneColumn'>
                <div className="smallBox19">
                    <div className="margin1">
                        <span className="clickRegisterText">From</span>
                        <input className="form-control enterDiv2" type="date" onChange={fromDateValidation}></input>
                    </div>
                    <div className="margin1">
                        <span className="clickRegisterText">To</span>
                        <input className="form-control enterDiv2" type="date" onChange={toDateValidation}></input>
                    </div>
                </div>
                <div className="smallBox25">
                    <span id="generateButton" className="btn btn-outline-success pointer disabled" onClick={getTransactionsBetweenTwoDates}>
                        <span>Get Transactions</span>
                    </span>
                </div>
            </div>}
        </div>
        
    );
}

export default FilterTransaction;