import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { updateEmail } from '../../../emailSlice';

function AdminMenu(){

    var [clicked,setClicked] = useState([false,false,false]);
    var dispatch = useDispatch();
    var navigate = useNavigate();

    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("id");
        navigate("/");
    }

    function navigateToCustomers(){
        dispatch(
            updateEmail("")
        )
        setClicked([true,false,false]);
        navigate("/adminMenu/allCustomers");
    }

    function navigateToEmployees(){
        dispatch(
            updateEmail("")
        )
        setClicked([false,true,false]);
        navigate("/adminMenu/allEmployees");
    }

    function navigateToProfile(){
        setClicked([false,false,true]);
        navigate("/adminMenu/adminProfile");
    }

    function openNav(){
        document.getElementById("mySidebar").style.width = "250px";
        document.getElementById("main").style.marginLeft = "0";
    };
      
    function closeNav(){
        document.getElementById("mySidebar").style.width = "0";
        document.getElementById("main").style.marginLeft= "0";
    };

    return (
        <div className="container">
            <div className="row">
                <div className="smallBox50 col-md-3">
                    <div className="smallBox51">
                        <div className="flexRow4">
                            <div className="logoImage change-my-color3"></div>
                            <span className="logo">Mavericks Bank</span>
                        </div>
                        <ul className="smallBox52 nav">
                            <div className="flexRow3">
                                <div className="beneficiary change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[0] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToCustomers}>Customers</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToCustomers}>Customers</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="account change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[1] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToEmployees}>Employees</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToEmployees}>Employees</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="profile change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[2] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToProfile}>Profile</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToProfile}>Profile</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="signout change-my-color3"></div>
                                <li className="nav-item highlight">
                                    <a className="nav-link textDecoWhite" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</a>
                                </li>
                            </div>
                        </ul>
                    </div>
                    <div className="modal fade" id="modal2" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h6 className="modal-title" id="modalEg1">Sign Out</h6>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="No"></button>
                                </div>
                                <div className="modal-body">
                                    Are you sure you want to Sign Out?
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                                    <a className="btn btn-outline-success" onClick={removeSession} data-bs-dismiss="modal">Sign Out</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="mobileMenu">
                    <div id="mySidebar" className="sidebar">
                        <span className="closebtn cancel change-my-color pointer" onClick={closeNav}>x</span>
                        <div className="smallBox62">
                        <div className="flexRow4">
                            <div className="logoImage change-my-color3"></div>
                            <span className="logo">mavericks</span>
                        </div>
                            <div className='flexColumn4'>
                                <ul className="smallBox52 nav">
                                    <div className="flexRow3">
                                        <div className="beneficiary change-my-color3"></div>
                                        <li className="nav-item highlight">
                                            {clicked[0] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToCustomers}>Customers</span> :
                                            <span className="nav-link textDecoWhite pointer" onClick={navigateToCustomers}>Customers</span>}
                                        </li>
                                    </div>
                                    <hr className="navBarLine"></hr>
                                    <div className="flexRow3">
                                        <div className="account change-my-color3"></div>
                                        <li className="nav-item highlight">
                                            {clicked[1] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToEmployees}>Employees</span> :
                                            <span className="nav-link textDecoWhite pointer" onClick={navigateToEmployees}>Employees</span>}
                                        </li>
                                    </div>
                                    <hr className="navBarLine"></hr>
                                    <div className="flexRow3">
                                        <div className="profile change-my-color3"></div>
                                        <li className="nav-item highlight">
                                            {clicked[2] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToProfile}>Profile</span> :
                                            <span className="nav-link textDecoWhite pointer" onClick={navigateToProfile}>Profile</span>}
                                        </li>
                                    </div>
                                    <hr className="navBarLine"></hr>
                                    <div className="flexRow3">
                                        <div className="signout change-my-color3"></div>
                                        <li className="nav-item highlight">
                                            <span className="nav-link textDecoWhite pointer" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</span>
                                        </li>
                                    </div>
                                </ul>
                            </div> 
                        </div>
                    </div>
                    <div id="main">
                        <div className="menu change-my-color3 change-my-color pointer" onClick={openNav}></div>
                    </div>
                    <div className="modal fade" id="modal2" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h6 className="modal-title" id="modalEg1">Sign Out</h6>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="No"></button>
                                </div>
                                <div className="modal-body">
                                    Are you sure you want to Sign Out?
                                 </div>
                                 <div className="modal-footer">
                                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal" onClick={openNav}>Back</button>
                                    <a className="btn btn-outline-success" onClick={removeSession} data-bs-dismiss="modal">Sign Out</a>
                                </div>
                            </div>
                        </div>
                    </div> 
                </div>
                <Outlet/>
            </div>
        </div>
    );
}

export default AdminMenu;