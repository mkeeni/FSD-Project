import './App.css';
import Landing from './Components/Landing/Login/Login';
import AllLoans from './Components/Loans/AllLoans/AllLoans';
import RegisterCustomer from './Components/Landing/RegisterCustomer/RegisterCustomer';
import ForgotPassword from './Components/Landing/ForgotPassword/ForgotPassword';
import AllCustomerAccounts from './Components/Accounts/AllCustomerAccounts/AllCustomerAccounts';
import AllCustomerBeneficiaries from './Components/Beneficiaries/AllCustomerBeneficiaries/AllCustomerBeneficiaries';
import AllCustomerTransactions from './Components/Transactions/AllCustomerTransactions/AllCustomerTransactions';
import { BrowserRouter, Route ,Routes} from 'react-router-dom';
import InvalidPage from './Components/Errors/InvalidPage/InvalidPage';
import LoginRoute from './Components/PrivateRoutes/LoginRoute';
import AvailedLoans from './Components/Loans/AvailedLoans/AvailedLoans';
import Menu from './Components/Menu/Menu';
import ViewAccount from './Components/Accounts/ViewAccount/ViewAccount';
import OpenNewAccount from './Components/Accounts/OpenNewAccount/OpenNewAccount';
import AddBeneficiary from './Components/Beneficiaries/AddBeneficiary/AddBeneficiary';
import ApplyLoan from './Components/Loans/ApplyLoan/ApplyLoan';
import ViewTransaction from './Components/Transactions/ViewTransaction/ViewTransaction';
import TransferMoney from './Components/Transactions/TransferMoney/TransferMoney';
import DepositMoney from './Components/Transactions/DepositMoney/DepositMoney';
import Withdraw from './Components/Transactions/Withdraw/Withdraw';
import Profile from './Components/Profile/Profile';
import RecentTransaction from './Components/Accounts/ViewAccount/RecentTransaction/RecentTransaction';
import LastMonthTransaction from './Components/Accounts/ViewAccount/LastMonthTransaction/LastMonthTransaction';
import FilterTransaction from './Components/Accounts/ViewAccount/FilterTransaction.js/FilterTransaction';
import DashBoard from './Components/Dashboard/Dashboard';
import AccountStatement from './Components/Dashboard/AccountStatement/AccountStatement';
import EmployeeMenu from './Components/Employees/EmployeeMenu/EmployeeMenu';
import Accounts from './Components/Employees/Accounts/Accounts';
import OpenRequests from './Components/Employees/Accounts/OpenRequests/OpenRequests';
import CloseRequests from './Components/Employees/Accounts/CloseRequests/CloseRequests';
import ViewCustomerAccount from './Components/Employees/Accounts/ViewCustomerAccount/ViewCustomerAccount';
import ViewCustomerRequest from './Components/Employees/Accounts/ViewCustomerRequest/ViewCustomerRequest';
import ViewCustomerAccountDetails from './Components/Employees/Accounts/ViewCustomerAccountDetails/ViewCustomerAccountDetails';
import AccountTransaction from './Components/Employees/Accounts/ViewCustomerAccountDetails/AccountTransactions/AccountTransactions';
import CustomerTransactions from './Components/Employees/Accounts/ViewCustomerAccountDetails/CustomerTransactions/CustomerTransactions';
import InboudsOutbounds from './Components/Employees/Accounts/ViewCustomerAccountDetails/InboundsOutbounds/InboudsOutbounds';
import Loans from './Components/Employees/Loans/Loans';
import LoanApplicationDetails from './Components/Employees/Loans/LoanApplicationDetails/LoanApplicationDetails';
import EmployeeProfile from './Components/Employees/EmployeeProfile/EmployeeProfile';
import AdminMenu from './Components/Admin/AdminMenu/AdminMenu';
import CreateCustomer from './Components/Admin/Customers/CreateCustomer/CreateCustomer';
import ViewCustomer from './Components/Admin/Customers/ViewCustomer/ViewCustomer';
import AllCustomers from './Components/Admin/Customers/AllCustomers/AllCustomers';
import AdminProfile from './Components/Admin/AdminProfile/AdminProfile';
import AllEmployees from './Components/Admin/Employees/AllEmployees/AllEmployees';
import CreateEmployee from './Components/Admin/Employees/CreateEmployee/CreateEmployee';
import ViewEmployee from './Components/Admin/Employees/ViewEmployee/ViewEmployee';
import ViewCustomerDetails from './Components/Employees/Customers/ViewCustomerDetails/ViewCustomerDetails';

function App() {
  return (
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<LoginRoute/>}/>
            <Route path="forgotPassword" element={<ForgotPassword/>}/>
            <Route path="registerCustomer" element={<RegisterCustomer/>}/>
            <Route path="menu" element={<Menu/>}>
                <Route path="dashboard" element={<DashBoard/>}/>
                <Route path="statement" element={<AccountStatement/>}/>
                <Route path="customerAccounts" element={<AllCustomerAccounts/>}/>
                <Route path="viewAccount" element={<ViewAccount/>}>
                    <Route index element={<RecentTransaction/>}/>
                    <Route path="recentTransaction" element={<RecentTransaction/>}/>
                    <Route path="lastMonthTransaction" element={<LastMonthTransaction/>}/>
                    <Route path="filterTransaction" element={<FilterTransaction/>}/>
                </Route>
                <Route path="openAccount" element={<OpenNewAccount/>}/>
                <Route path="customerTransactions" element={<AllCustomerTransactions/>}/>
                <Route path="viewTransaction" element={<ViewTransaction/>}/>
                <Route path="transferMoney" element={<TransferMoney/>}/>
                <Route path="depositMoney" element={<DepositMoney/>}/>
                <Route path="withdrawMoney" element={<Withdraw/>}/>
                <Route path="allLoans" element={<AllLoans/>}/>
                <Route path="availedLoans" element={<AvailedLoans/>}/>
                <Route path="applyLoan" element={<ApplyLoan/>}/>
                <Route path="customerBeneficiaries" element={<AllCustomerBeneficiaries/>}/>
                <Route path="addBeneficiary" element={<AddBeneficiary/>}/>
                <Route path="profile" element={<Profile/>}/>
            </Route>
            <Route path="employeeMenu" element={<EmployeeMenu/>}>
                <Route path="accounts" element={<Accounts/>}>
                    <Route index element={<ViewCustomerAccount/>}/>
                    <Route path="openRequests" element={<OpenRequests/>}/>
                    <Route path="closeRequests" element={<CloseRequests/>}/>
                    <Route path="viewCustomerAccount" element={<ViewCustomerAccount/>}/>
                </Route>
                <Route path="viewOpenRequest" element={<ViewCustomerRequest/>}/>
                <Route path="viewDetails" element={<ViewCustomerAccountDetails/>}>
                    <Route index element={<AccountTransaction/>}/>
                    <Route path="accountTransactions" element={<AccountTransaction/>}/>
                    <Route path="inboudsOutbounds" element={<InboudsOutbounds/>}/>
                    <Route path="customerTransactions" element={<CustomerTransactions/>}/>
                </Route>
                <Route path="viewTransaction" element={<ViewTransaction/>}/>
                <Route path="loans" element={<Loans/>}/>
                <Route path="viewLoan" element={<LoanApplicationDetails/>}/>
                <Route path="allCustomers" element={<AllCustomers/>}/>
                <Route path="viewCustomerDetails" element={<ViewCustomerDetails/>}/>
                <Route path="employeeProfile" element={<EmployeeProfile/>}/>
            </Route>
            <Route path="adminMenu" element={<AdminMenu/>}>
                <Route path="allCustomers" element={<AllCustomers/>}/>
                <Route path="createCustomer" element={<CreateCustomer/>}/>
                <Route path="viewCustomer" element={<ViewCustomer/>}/>
                <Route path="allEmployees" element={<AllEmployees/>}/>
                <Route path="createEmployee" element={<CreateEmployee/>}/>
                <Route path="viewEmployee" element={<ViewEmployee/>}/>
                <Route path="adminProfile" element={<AdminProfile/>}/>
            </Route>
            <Route path="*" element={<InvalidPage/>}/>
        </Routes>
    </BrowserRouter>
  );
}

export default App;
