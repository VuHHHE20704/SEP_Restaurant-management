import { Button, Checkbox, Divider, Form, Input, Space, Typography } from 'antd'
import { LockOutlined, MailOutlined } from '@ant-design/icons'
import { Link } from 'react-router-dom'
import AuthCardWrapper from '../Component/AuthCardWrapper'
import { ROUTES } from '../Constants/routes'
import { loginTips } from '../Feature/auth/authContent'
import { usePageTitle } from '../hooks/usePageTitle'

function LoginPage() {
  usePageTitle('Login')

  const onFinish = (values) => {
    // eslint-disable-next-line no-console
    console.log('Login submit:', values)
  }

  return (
    <AuthCardWrapper
      title="Login"
      subtitle="Sign in to access Hola Restaurant operations and management tools."
    >
      <Space direction="vertical" size={12} className="full-width">
        <Form layout="vertical" onFinish={onFinish} requiredMark={false}>
          <Form.Item
            label="Email"
            name="email"
            rules={[
              { required: true, message: 'Please enter your email.' },
              { type: 'email', message: 'Email format is invalid.' },
            ]}
          >
            <Input prefix={<MailOutlined />} placeholder="staff@holarestaurant.vn" size="large" />
          </Form.Item>

          <Form.Item
            label="Password"
            name="password"
            rules={[{ required: true, message: 'Please enter your password.' }]}
          >
            <Input.Password prefix={<LockOutlined />} placeholder="Enter password" size="large" />
          </Form.Item>

          <Form.Item name="remember" valuePropName="checked">
            <Checkbox>Remember me</Checkbox>
          </Form.Item>

          <Form.Item className="m0">
            <Button type="primary" htmlType="submit" size="large" block>
              Login
            </Button>
          </Form.Item>
        </Form>

        <Divider className="compact-divider" />

        <Typography.Text strong>After login you can:</Typography.Text>
        <ul className="tips-list">
          {loginTips.map((tip) => (
            <li key={tip}>{tip}</li>
          ))}
        </ul>

        <Typography.Paragraph className="m0">
          Forgot your password? <Link to={ROUTES.FORGET_PASSWORD}>Reset it here</Link>
        </Typography.Paragraph>
      </Space>
    </AuthCardWrapper>
  )
}

export default LoginPage
