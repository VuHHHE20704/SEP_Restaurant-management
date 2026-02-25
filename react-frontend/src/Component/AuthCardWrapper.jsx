import { Card, Typography } from 'antd'

function AuthCardWrapper({ title, subtitle, children }) {
  return (
    <div className="auth-page">
      <Card className="auth-card">
        <Typography.Title level={2} className="auth-title">
          {title}
        </Typography.Title>
        <Typography.Paragraph className="auth-subtitle">
          {subtitle}
        </Typography.Paragraph>
        {children}
      </Card>
    </div>
  )
}

export default AuthCardWrapper
