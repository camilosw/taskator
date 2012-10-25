class Project < ActiveRecord::Base
  belongs_to :client
  has_many :tasks
  attr_accessible :closed, :description, :name, :order, :client_id

  validates :name, presence: true
  validates :client_id, presence: true

  def self.active
    projects = Project.where(closed: false)
  end
end

# == Schema Information
#
# Table name: projects
#
#  id          :integer          not null, primary key
#  client_id   :integer
#  name        :string(255)      not null
#  description :text
#  order       :integer
#  closed      :boolean          default(FALSE)
#  created_at  :datetime         not null
#  updated_at  :datetime         not null
#

