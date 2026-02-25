import { Layout, Menu, Typography } from 'antd'
import { HomeOutlined, KeyOutlined, LoginOutlined, ShopOutlined } from '@ant-design/icons'
import { Link, useLocation } from 'react-router-dom'
import { ROUTES } from '../Constants/routes'

const { Header, Content, Footer } = Layout

const navItems = [
  {
    key: ROUTES.HOME,
    icon: <HomeOutlined />,
    label: <Link to={ROUTES.HOME}>Home</Link>,
  },
  {
    key: ROUTES.LOGIN,
    icon: <LoginOutlined />,
    label: <Link to={ROUTES.LOGIN}>Login</Link>,
  },
  {
    key: ROUTES.FORGET_PASSWORD,
    icon: <KeyOutlined />,
    label: <Link to={ROUTES.FORGET_PASSWORD}>Forget Password</Link>,
  },
]

function AppLayoutShell({ children }) {
  const location = useLocation()
  const selectedKey = navItems.some((item) => item.key === location.pathname)
    ? location.pathname
    : ROUTES.HOME

  return (
    <Layout className="app-shell">
      <Header className="app-header">
        <div className="brand-wrap">
          <ShopOutlined className="brand-icon" />
          <Typography.Text className="brand-text">Hola Restaurant</Typography.Text>
        </div>

        <Menu
          mode="horizontal"
          items={navItems}
          selectedKeys={[selectedKey]}
          className="nav-menu"
          overflowedIndicator={null}
        />
      </Header>

      <Content className="app-content">{children}</Content>

      <Footer className="app-footer">
        Hola Restaurant © {new Date().getFullYear()} | Restaurant Portal
      </Footer>
    </Layout>
  )
}

export default AppLayoutShell
