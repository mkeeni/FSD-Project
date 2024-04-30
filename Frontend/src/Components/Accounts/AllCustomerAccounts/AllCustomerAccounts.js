import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Account from '../Account/Account';
import { useDispatch } from 'react-redux';
import { updateDate } from '../../../dateSlice';

function AllCustomerAccounts(){

    var [accounts,setAccounts] = useState([]);

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var dispatch = useDispatch();

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        dispatch(
            updateDate({
                "fromDate": "",
                "toDate": ""
            })
        )
        allAccounts();
    },[]);

    async function allAccounts(){
        await axios.get('http://localhost:7289/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
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
        <div className="smallBox17 col-md-9">
            <div className="smallBox21">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoBlack smallBox23" to="/menu/customerAccounts">All Accounts</Link>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoOrange smallBox23" to="/menu/openAccount">Open New Account</Link>
                    </li>
                </ul>
                {error ? 
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterTextBlack">{errorMessage}</div>
                </div> : 
                <div className="scrolling">
                {accounts.map(account => 
                    <Account key = {account.accountID} account={account}/>
                )}
                </div>}
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Delete Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to delete this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Delete</button>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    );
}

export default AllCustomerAccounts;