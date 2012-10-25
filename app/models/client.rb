class Client < ActiveRecord::Base
  has_many :projects
  attr_accessible :closed, :name

  validates :name, presence: true

  def self.active
    clients = Client.where(closed: false)
  end
end

# == Schema Information
#
# Table name: clients
#
#  id         :integer          not null, primary key
#  name       :string(255)      not null
#  closed     :boolean          default(FALSE)
#  created_at :datetime         not null
#  updated_at :datetime         not null
#

