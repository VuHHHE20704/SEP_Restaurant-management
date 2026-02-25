import { Button, Form, Input, Space, Typography } from 'antd'
import { MailOutlined } from '@ant-design/icons'
import { Link } from 'react-router-dom'
import AuthCardWrapper from '../Component/AuthCardWrapper'
import { ROUTES } from '../Constants/routes'
import { forgotPasswordNotice } from '../Feature/auth/authContent'
import { usePageTitle } from '../hooks/usePageTitle'

function ForgetPasswordPage() {
  usePageTitle('Forget Password')

  const onFinish = (values) => {
    // eslint-disable-next-line no-console
    console.log('Reset password request:', values)
  }

  return (
    <AuthCardWrapper title="Forget Password" subtitle={forgotPasswordNotice}>
      <Space direction="vertical" size={12} className="full-width">
        <Form layout="vertical" onFinish={onFinish} requiredMark={false}>
          <Form.Item
            label="Registered Email"
            name="email"
            rules={[
              { required: true, message: 'Please enter your registered email.' },
              { type: 'email', message: 'Email format is invalid.' },
            ]}
          >
            <Input
              size="large"
              prefix={<MailOutlined />}
              placeholder="your-email@holarestaurant.vn"
            />
          </Form.Item>

          <Form.Item className="m0">
            <Button type="primary" htmlType="submit" size="large" block>
              Send Reset Link
            </Button>
          </Form.Item>
        </Form>

        <Typography.Paragraph className="m0">
          Back to <Link to={ROUTES.LOGIN}>Login</Link> or <Link to={ROUTES.HOME}>Home</Link>
        </Typography.Paragraph>
      </Space>
    </AuthCardWrapper>
  )
}

export default ForgetPasswordPage
