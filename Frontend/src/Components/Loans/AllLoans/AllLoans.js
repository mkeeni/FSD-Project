import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import Loan from '../Loan/Loan';

function AllLoans(){

    var [loans,setloans] = useState(
        [{
            "loanID": 0,
            "loanAmount": "",
            "loanType": "",
            "interest": "",
            "tenure": "",
        }]
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allLoans();
    },[])

    async function allLoans(){
        await axios.get('http://localhost:7289/api/Loans/GetAllLoans',httpHeader)
        .then(function (response) {
            setloans(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }
                
    return(
        <div className="smallBox17 col-md-9 scrolling">
            <div className="smallBox26">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoBlack smallBox23" to="/menu/allLoans">All Loans</Link>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoOrange smallBox23" to="/menu/availedLoans">Availed Loans</Link>
                    </li>
                </ul>
                <div className="scrolling">
                    {loans.map(loan => 
                        <Loan key = {loan.loanID} loan = {loan}/>
                    )
                    }
                </div>
            </div>
        </div>
    );
}

export default AllLoans;