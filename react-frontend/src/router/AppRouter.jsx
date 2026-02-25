import { BrowserRouter, Navigate, useRoutes } from 'react-router-dom'
import AppLayoutShell from '../Component/AppLayoutShell'
import { ROUTES } from '../Constants/routes'
import ForgetPasswordPage from '../page/ForgetPasswordPage'
import HomePage from '../page/HomePage'
import LoginPage from '../page/LoginPage'

function RouterView() {
  const routes = [
    { path: ROUTES.HOME, element: <HomePage /> },
    { path: ROUTES.LOGIN, element: <LoginPage /> },
    { path: ROUTES.FORGET_PASSWORD, element: <ForgetPasswordPage /> },
    { path: '*', element: <Navigate to={ROUTES.HOME} replace /> },
  ]

  const content = useRoutes(routes)

  return <AppLayoutShell>{content}</AppLayoutShell>
}

function AppRouter() {
  return (
    <BrowserRouter>
      <RouterView />
    </BrowserRouter>
  )
}

export default AppRouter
