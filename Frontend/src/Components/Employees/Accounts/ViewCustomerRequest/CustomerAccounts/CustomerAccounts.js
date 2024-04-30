import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CustomerAccounts(props) {

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [accounts,setAccounts] = useState([]);

    useEffect(() => {
        getCustomerAccounts();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerAccounts() {
        await axios.get('http://localhost:7289/api/Accounts/GetAllCustomerAccounts?customerID=' + props.account.customerID, httpHeader)
            .then(function (response) {
                setAccounts(response.data);
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
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> : 
                <div>
                    {accounts.map(account => 
                        <div key={account.accountID} className="whiteOutlineBox6">
                            <div className="whiteOutlineBoxMargin">
                                <span className="clickRegisterText">Account No: {account.accountNumber}</span>
                                <span className="clickRegisterText">Account type: {account.accountType}</span>
                                <span className="clickRegisterText">Branch Name: {account.branches.branchName}</span>
                                <span className="clickRegisterText13">Status: {account.status}</span>
                            </div>
                        </div>
                    )}
                </div>}
        </div>
    );
}

export default CustomerAccounts;