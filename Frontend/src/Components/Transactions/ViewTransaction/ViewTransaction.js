import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate} from 'react-router-dom';
import { useSelector } from 'react-redux';

function ViewTransaction(){

    var transactionID = useSelector((state) => state.transactionID);
    var navigate = useNavigate();

    var [transaction,setTransaction] = useState(
        {
            "transactionID": 0,
            "amount": 0,
            "transactionDate": "2024-02-21T08:58:09.162Z",
            "description": "string",
            "transactionType": "string",
            "accounts": {
              "branches": {
                "banks": {
                  "bankName": "string"
                }
              },
              "customers": {
                "name": "string",
              }
            },
            "beneficiaries": {
              "name": "string",
              "branches": {
                "banks": {
                  "bankName": "string"
                }
              },
            }
        }
    );

    const email = sessionStorage.getItem('email');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getTransaction();
    },[])

    async function getTransaction(){
      if(transactionID === 0){
        if(email.includes("@maverick.in")){
          navigate("/employeeMenu/viewDetails/accountTransactions");
        }
        else{
          navigate("/menu/customerTransactions")
        }
      }
      else{
        await axios.get('http://localhost:7289/api/Transactions/GetTransaction?transactionID=' + transactionID,httpHeader).then(function (response) {
              setTransaction(response.data);
          })
          .catch(function (error) {
              console.log(error);
          })
      }
    }

    function navigateBack(){
      navigate(-1);
    }

    return (
        <div className="smallBox17 col-md-9">
                {transaction.transactionType === "Transfer" ? 
                  <div className="smallBox26">
                    <div className="upMargin2">
                      <span className="pointer" onClick={navigateBack}>
                        <div className="leftArrow change-my-color"></div>
                      </span>
                    </div>
                    <span className="clickRegisterText8">{transaction.amount}</span>
                    {transaction.status === "Failed" ? <span className="clickRegisterText9">Transaction Failed</span> : null}
                    {transaction.description === "" ? null : <span className="clickRegisterText7">Description: {transaction.description}</span>}
                    <span className="clickRegisterText7">{transaction.transactionType} To {transaction.beneficiaries.name}</span>
                    <span className="clickRegisterText7">{transaction.beneficiaries.branches.banks.bankName}</span>
                    <hr className="hr3"></hr>
                    <span className="clickRegisterText7">From</span>
                    <span className="clickRegisterText7">{transaction.accounts.customers.name}</span>
                    <span className="clickRegisterText7">{transaction.accounts.branches.banks.bankName}</span>
                    <hr className="hr3"></hr>
                    <span className="clickRegisterText7">Paid at {transaction.transactionDate}</span>
                    <span className="clickRegisterText7">Transaction ID: {transaction.transactionID}</span>
                  </div> : 
                  <div className="smallBox26">
                    <div className="upMargin2">
                      <span className="pointer" onClick={navigateBack}>
                        <div className="leftArrow change-my-color"></div>
                      </span>
                    </div>
                    <span className="clickRegisterText8">{transaction.amount}</span>
                    {transaction.status === "Failed" ? <span className="clickRegisterText9">Transaction Failed</span> : null}
                    {transaction.description === "" ? null : <span className="clickRegisterText7">Description: {transaction.description}</span>}
                    <span className="clickRegisterText7">{transaction.transactionType}</span>
                    {transaction.transactionType === "Deposit" ? 
                    <span className="clickRegisterText7">To {transaction.accounts.branches.banks.bankName}</span> : 
                    <span className="clickRegisterText7">From {transaction.accounts.branches.banks.bankName}</span>}
                    <hr className="hr3"></hr>
                    <span className="clickRegisterText7">Paid at {transaction.transactionDate}</span>
                    <span className="clickRegisterText7">Transaction ID: {transaction.transactionID}</span>
                  </div>
                }
                
        </div>
    );
}

export default ViewTransaction;