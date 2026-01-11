// import { render, screen } from '@testing-library/react';
// import { Button } from '@/compoents//Button';

// describe('Button', () => {
//   it('renders button with text', () => {
//     render(<Button>Click me</Button>);
//     expect(screen.getByText('Click me')).toBeInTheDocument();
//   });

//   it('applies primary variant styles by default', () => {
//     render(<Button>Primary</Button>);
//     const button = screen.getByText('Primary');
//     expect(button).toHaveClass('bg-blue-600');
//   });

//   it('applies secondary variant styles', () => {
//     render(<Button variant="secondary">Secondary</Button>);
//     const button = screen.getByText('Secondary');
//     expect(button).toHaveClass('bg-gray-200');
//   });

//   it('handles click events', () => {
//     const handleClick = jest.fn();
//     render(<Button onClick={handleClick}>Click</Button>);
//     screen.getByText('Click').click();
//     expect(handleClick).toHaveBeenCalledTimes(1);
//   });
// });