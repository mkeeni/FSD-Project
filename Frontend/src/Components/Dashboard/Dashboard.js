import axios from 'axios';
import { useEffect, useState } from "react";
import '../style.css';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { updateStatement } from '../../statementSlice';

function DashBoard() {

    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");
    var [report,setReport] = useState({});
    var [reportFetched,setReportFetched] = useState(false);
    var [fromDate,setFromDate] = useState("");
    var [toDate,setToDate] = useState("");
    var [availedLoans,setAvailedLoans] = useState([]);
    var [beneficiaries,setBeneficiaries] = useState([]);
    var [customer,setCustomer] = useState({});
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var statementData = {
        "accountID": accountID,
        "fromDate": fromDate,
        "toDate": toDate
    }

    var navigate = useNavigate(); 
    var dispatch = useDispatch();

    var customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getCustomer();
        getAllCustomerAccounts();
        allAvailedLoans();
        allBeneficiaries();
        getCustomerReport();
    },[])

    async function navigateToAccountStatement(){
        dispatch(
            updateStatement(statementData)
        );
        navigate("/menu/statement");
    }

    async function getAllCustomerAccounts(){
        await axios.get('http://localhost:7289/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
        .then(function (response) {
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function allAvailedLoans(){
        await axios.get('http://localhost:7289/api/AppliedLoans/GetAllCustomerAvailedLoans?customerID=' + customerID,httpHeader).then(function (response) {
            setAvailedLoans(response.data);
        })
        .catch(function (error) {
            console.log(error);
         })
    }

    async function allBeneficiaries(){
        await axios.get('http://localhost:7289/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=' + customerID,httpHeader).then(function (response) {
            setBeneficiaries(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })  
    }

    async function getCustomer(){
        await axios.get('http://localhost:7289/api/Customers/GetCustomer?customerID=' + customerID,httpHeader).then(function (response) {
            setCustomer(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })  
    }

    async function getCustomerReport(){
        await axios.get('http://localhost:7289/api/Transactions/CustomerRegulatoryReport?customerID=' + customerID,httpHeader)
        .then(function (response) {
            setReport(response.data);
            setReportFetched(true);
        })
        .catch(function (error) {
            console.log(error);
            setReportFetched(false);
        })
    }

    function fromDateValidation(eventargs){
        var fromDate = eventargs.target.value;
        setFromDate(fromDate);
        if(fromDate !== "" && toDate !== "" && accountID !== ""){
            document.getElementById("generateButton").classList.remove("disabled");
        }
        else{
            document.getElementById("generateButton").classList.add("disabled");
        }
    }

    function toDateValidation(eventargs){
        var toDate = eventargs.target.value;
        setToDate(toDate);
        if(fromDate !== "" && toDate !== "" && accountID !== ""){
            document.getElementById("generateButton").classList.remove("disabled");
        }
        else{
            document.getElementById("generateButton").classList.add("disabled");
        }
    }

    function accountValidation(eventargs){
        var accountID = eventargs.target.value;
        setAccountID(accountID);
        if(accountID !== ""){
            setError(false);
            if(accountID !== "" &&fromDate !== "" && toDate !== ""){
                document.getElementById("generateButton").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please select an account");
            document.getElementById("generateButton").classList.add("disabled");
        }
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <div className="flexRow2">
                    <span className="clickRegisterText5">Welcome {customer.name}</span>
                </div>
                <hr className="hrS"></hr>
                <div className="flexRow6">
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Profile Strength</span>
                            <span className="clickRegisterText3">100%</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Total Accounts</span>
                            <span className="clickRegisterText3">{accounts.length}</span>
                        </div>
                    </div>
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Active Loans</span>
                            <span className="clickRegisterText3">{availedLoans.length}</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Beneficiaries</span>
                            <span className="clickRegisterText3">{beneficiaries.length}</span>
                        </div>
                    </div>
                </div>
                {reportFetched ? 
                <div className="flexRow6">
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Credit Status</span>
                            {report.creditWorthiness === "Yes" ? <span className="clickRegisterText3">Good</span> :
                            <span className="clickRegisterText3">Bad</span>}
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Inbounds</span>
                            <span className="clickRegisterText3">{report.inboundTransactions}</span>
                        </div>
                    </div>
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Outbounds</span>
                            <span className="clickRegisterText3">{report.outboundTransactions}</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Credit Score</span>
                            <span className="clickRegisterText3">{report.ratio}</span>
                        </div>
                    </div>
                </div> : 
                <div className="flexRow6">
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Credit Status</span>
                            <span className="clickRegisterText3">--</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Inbounds</span>
                            <span className="clickRegisterText3">--</span>
                        </div>
                    </div>
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Outbounds</span>
                            <span className="clickRegisterText3">--</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterTextBlack">Credit Score</span>
                            <span className="clickRegisterText3">--</span>
                        </div>
                    </div>
                </div>}
                
                <hr className="hrS"></hr>
                <span className="clickRegisterText10">Generate Account Statement</span>
                <div className='phoneMargin2'>
                    <div className="smallBox19 phoneMargin2">
                        <div className="margin1">
                            <span className="clickRegisterTextBlack">From</span>
                            <input className="form-control enterDiv2" type="date" onChange={fromDateValidation}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterTextBlack">To</span>
                            <input className="form-control enterDiv2" type="date" onChange={toDateValidation}></input>
                        </div>
                    </div>
                    <div className="smallBox25">
                        <div className='phoneMargin'>
                            <span className="clickRegisterTextBlack">Select Account</span>
                            <select className="form-control enterDiv2" value={accountID} onChange={accountValidation}>
                                <option value="">Select</option>
                                {accounts.map(account =>
                                    <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                                )}
                            </select>
                        </div>
                        <span id="generateButton" className="btn btn-outline-success buttonWidth pointer disabled" onClick={navigateToAccountStatement}>
                            <span>Generate Report</span>
                        </span>
                    </div>
                    {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                </div>
            </div>
        </div>
    );
}

export default DashBoard;